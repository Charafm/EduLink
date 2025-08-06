using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.Configuration;
using SchoolSaas.Domain.Common.Constants;
using SchoolSaas.Infrastructure.Common;
using SchoolSaas.Infrastructure.Common.Context;
using SchoolSaas.Infrastructure.Common.Logger;
using SchoolSaas.Infrastructure.Common.MultiTenancy.Services;
using SchoolSaas.Infrastructure.Common.Services;
using RazorViewToStringRenderer = SchoolSaas.Infrastructure.Common.Services.RazorViewToStringRenderer;

namespace SchoolSaas.Infrastructure.Common
{
    public static class InfrastructureDependencyInjection
    {
        public static IServiceCollection AddContext<TContext, TIContext>(this IServiceCollection services, IConfiguration configuration)
            where TContext : DbContext, TIContext
            where TIContext : class
        {
            var connectionString = configuration.GetConnectionString(typeof(TContext).Name);
            services.AddDbContext<TContext>(options =>
                options.UseSqlServer(
                    connectionString,
                    b => b.MigrationsAssembly(typeof(TContext).Assembly.FullName)).AddInterceptors(new CommandInterceptor()), ServiceLifetime.Transient);

            services.AddScoped<TIContext>(provider => provider.GetService<TContext>()!);

            return services;
        }

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<SecurityOptions>(configuration.GetSection(ConfigurationConstants.Sections.Security));
            services.Configure<EmailOptions>(configuration.GetSection(ConfigurationConstants.Sections.Mail));
            services.Configure<WebPortalsURLsOprions>(configuration.GetSection(ConfigurationConstants.Sections.WebPortalsURLs));
            services.Configure<ErrorDetailsOptions>(configuration.GetSection(ConfigurationConstants.Sections.ErrorDetails));
            services.AddTransient<IServiceHelper, ServiceHelper>();
            
            services.AddTransient<IEdulinkServiceHelper, EdulinkServiceHelper>();
            services.AddScoped<ITenantAccessor, TenantAccessor>();
            services.AddTransient<IDateTime, DateTimeService>();
            services.AddTransient<IStorage, LocalStorage>();
            services.AddTransient<ICacheService, CacheService>();
            services.AddTransient<IMediaService, MediaService>();
            services.AddScoped<ITemplateToStringRenderer, RazorViewToStringRenderer>();
            services.AddTransient<ISecurityTokenGenerator, DefaultSecurityTokenGenerator>();
            services.AddTransient<IPdfGenerationService, PdfGenerationService>();
            services.AddTransient<IFileScanService, ClamAVService>();
            services.AddTransient<IEmailSender, EmailSender>();

            return services;
        }

        public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var securityOptions = configuration.GetSection(ConfigurationConstants.Sections.Security)
                    .Get<SecurityOptions>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.Authority = securityOptions?.AuthorityUrl;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateAudience = false
                    };
                });

            services.AddAuthorization();

            return services;
        }

        public static ILoggingBuilder AddDbLogger(this ILoggingBuilder builder, Action<DbLoggerOptions> configure)
        {
            builder.Services.AddSingleton<IDbLoggerProvider, DbLoggerProvider>();
            builder.Services.Configure(configure);
            return builder;
        }
    }
}