using Autofac.Extensions.DependencyInjection;
using Autofac;

using Autofac.Multitenant;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NSwag;
using NSwag.Generation.Processors.Security;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Infrastructure.Common.Converters;
using SchoolSaas.Web.Common.Converters;
using SchoolSaas.Web.Common.Filters;
using SchoolSaas.Web.Common.Services;
using SchoolSaas.Web.Common.TenantResolutionStrategies;
using System.Globalization;

namespace SchoolSaas.Web.Common
{
    public static class WebApplicationBuilderExtensions
    {
        public static WebApplicationBuilder AddSchoolWebApplication(
            this WebApplicationBuilder builder, string appName, Action<IServiceCollection> configure, List<JsonConverter> converters = null)
        {
            var logFileName = $"{Directory.GetCurrentDirectory()}/logs/{appName}.log";
            var logOutputTemplate = "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}";

            //Log.Logger = new LoggerConfiguration()
            //    .MinimumLevel.Debug()
            //    .WriteTo.Console(outputTemplate: logOutputTemplate)
            //    .WriteTo.File(logFileName, rollingInterval: RollingInterval.Day)
            //    .Enrich.FromLogContext()
            //    .ReadFrom.Configuration(builder.Configuration)
            //    .CreateLogger();

            //builder.Host.UseSerilog((ctx, lc) => lc
            //.MinimumLevel.Debug()
            //.WriteTo.Console(outputTemplate: logOutputTemplate)
            //.WriteTo.File(logFileName, rollingInterval: RollingInterval.Day)
            //.Enrich.FromLogContext()
            //.ReadFrom.Configuration(ctx.Configuration));
            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            builder.Host.ConfigureContainer<ContainerBuilder>((context, builder) =>
            {
                ConfigureContainer(builder);
            });
            //// This adds the required middleware to the ROOT CONTAINER and is required for multitenancy to work.
            //builder.Services.AddAutofacMultitenantRequestServices();

            configure(builder.Services);

            builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
            builder.Services.AddHttpContextAccessor();

            builder.Services.AddHealthChecks();
            builder.Services.AddControllersWithViews(options =>
            {
                options.Filters.Add<ApiExceptionFilter>();
            })
                .AddNewtonsoftJson(opts =>
                {
                    opts.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    opts.SerializerSettings.Converters.Add(new StringEnumConverter());
                    opts.SerializerSettings.Converters.Add(new DecimalConverter());
                    opts.SerializerSettings.Converters.Add(new TimeOnlyJsonConverter());
                    opts.SerializerSettings.Converters.Add(new DateOnlyJsonConverter());
                    if (converters != null)
                    {
                        foreach (var converter in converters)
                        {
                            opts.SerializerSettings.Converters.Add(converter);
                        }
                    }

                });

            builder.Services.AddLocalization();
            builder.Services.AddRequestLocalization(x =>
            {
                x.DefaultRequestCulture = new RequestCulture("fr");
                x.ApplyCurrentCultureToResponseHeaders = true;
                x.SupportedCultures = new List<CultureInfo> { new("fr"), new("en") };
                x.SupportedUICultures = new List<CultureInfo> { new("fr"), new("en") };
            });

            builder.Services.AddRazorPages();

            builder.Services.Configure<RouteOptions>(options =>
            {
                options.LowercaseUrls = true;
            });
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddOpenApiDocument(configure =>
            {
                configure.Title = $"EduLink {appName} API";
                configure.AddSecurity("JWT", Enumerable.Empty<string>(), new OpenApiSecurityScheme
                {
                    Type = OpenApiSecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    In = OpenApiSecurityApiKeyLocation.Header,
                    Description = "Type into the textbox: Bearer {your JWT token}."
                });

                configure.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
            });

            var origins = builder.Configuration.GetValue<string>("AllowedOrigins", string.Empty).Split(",");
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder.WithOrigins(origins)
                        .SetIsOriginAllowedToAllowWildcardSubdomains()
                        .AllowAnyMethod()
                        .AllowCredentials()
                        .AllowAnyHeader();

                        builder.AllowAnyHeader();
                        builder.AllowAnyMethod();
                    });
            });

            builder.Services.AddLocalization();

            builder.Host.UseSystemd();

            return builder;
        }

        // ConfigureContainer is where you can register things directly
        // with Autofac. This runs after ConfigureServices so the things
        // here will override registrations made in ConfigureServices.
        // Don't build the container; that gets done for you by the factory
        public static void ConfigureContainer(ContainerBuilder builder)
        {
            // Register your own things directly with Autofac here. Don't
            // call builder.Populate(), that happens in AutofacServiceProviderFactory
            // for you.

            // This will all go in the ROOT CONTAINER and is NOT TENANT SPECIFIC.

            //builder.RegisterModule(new MyApplicationModule());

            //builder.RegisterType<Dependency>()
            //    .As<IDependency>()
            //    .WithProperty("Id", "base")
            //    .InstancePerLifetimeScope();
        }

        public static MultitenantContainer ConfigureMultitenantContainer(IContainer container)
        {
            // This is the MULTITENANT PART. Set up your tenant-specific stuff here.
            var strategy = new HeaderResolutionStrategy(
                container.Resolve<IHttpContextAccessor>(),
                container.Resolve<Microsoft.Extensions.Logging.ILogger<HeaderResolutionStrategy>>());

            var multitenantContainer = new MultitenantContainer(strategy, container);

            return multitenantContainer;
        }
    }
}
