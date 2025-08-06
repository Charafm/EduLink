using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.Constants;
using SchoolSaas.Infrastructure.Common.Context;
using SchoolSaas.Infrastructure.Identity.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenIddict.Validation.AspNetCore;
using static OpenIddict.Abstractions.OpenIddictConstants;
using OpenIddict.Abstractions;
using OpenIddict.EntityFrameworkCore.Models;
using SchoolSaas.Infrastructure.Identity.Identity.Stores;
using SchoolSaas.Infrastructure.Identity.Context;
using SchoolSaas.Infrastructure.Identity.Identity.Entities;
using SchoolSaas.Infrastructure.Identity.Helpers;

namespace SchoolSaas.Infrastructure.Identity
{
    public static class IdentityDependencyInjection
    {
        public static IServiceCollection AddIdentityInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IContextFactory, ContextFactory>();
            services.AddScoped<JwtTokenHandler>();
            services.AddScoped<ManagerPicker>();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<IFoPermissionService, FoPermissionService>();
            services.AddScoped<IStaffIdentityService, CitizenIdentityService>();
            services.AddSingleton<IOpenIddictTokenStore<OpenIddictEntityFrameworkCoreToken>, CustomTokenStore>();
            services.AddSingleton<IOpenIddictApplicationStore<OpenIddictEntityFrameworkCoreApplication>, CustomApplicationStore>();
            services.AddSingleton<IOpenIddictTokenStoreResolver, CustomTokenStoreResolver>();
            services.AddSingleton<IOpenIddictApplicationStoreResolver, CustomApplicationStoreResolver>(); 
            services.AddScoped<ITokenValidationService, TokenValidationService>();
            services.AddSingleton<CustomApplicationStoreResolver>();
            services.AddSingleton<CustomApplicationManager>();
            services.AddSingleton<IOpenIddictApplicationManager, CustomApplicationManager>();
            services.AddSingleton<IOpenIddictAuthorizationStore<OpenIddictEntityFrameworkCoreAuthorization>, CustomAuthorizationStore>();
            services.AddSingleton<IOpenIddictAuthorizationStoreResolver, CustomAuthorizationStoreResolver>();
          //  services.AddSingleton<IHubConnectionManager, HubConnectionManager>();

            services.AddScoped<IStaffIdentityService>(provider =>
            {
                var dbContext = provider.GetRequiredService<FrontOfficeIdentityContext>();
                var managerFactory = provider.GetRequiredService<ManagerPicker>();
                var emailSender = provider.GetRequiredService<IEmailSender>();
                var currentUserService = provider.GetRequiredService<ICurrentUserService>();
                var tenantAccessor = provider.GetRequiredService<ITenantAccessor>();
                return new CitizenIdentityService(dbContext, emailSender, currentUserService, tenantAccessor, managerFactory);
            });

            services.AddScoped<IIdentityService>(provider =>
            {
                var dbContext = provider.GetRequiredService<IdentityContext>();
                var managerFactory = provider.GetRequiredService<ManagerPicker>();
                var emailSender = provider.GetRequiredService<IEmailSender>();
                var currentUserService = provider.GetRequiredService<ICurrentUserService>();
                var tenantAccessor = provider.GetRequiredService<ITenantAccessor>();
                return new IdentityService(dbContext, emailSender, currentUserService, tenantAccessor, managerFactory);
            });

            services.AddDbContext<IdentityContext>(options =>
            {
                var connectionString = configuration.GetConnectionString("IdentityContext");
                options.UseSqlServer(connectionString)
                       .AddInterceptors(new CommandInterceptor())
                       .UseOpenIddict();
            });

            services.AddDbContext<FrontOfficeIdentityContext>(options =>
            {
                var connectionString = configuration.GetConnectionString("FrontOfficeIdentityContext");
                options.UseSqlServer(connectionString)
                       .AddInterceptors(new CommandInterceptor())
                       .UseOpenIddict();
            });

            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+ ";
            })
            .AddRoles<ApplicationRole>()
            .AddEntityFrameworkStores<IdentityContext>()
            .AddEntityFrameworkStores<FrontOfficeIdentityContext>()
            .AddDefaultTokenProviders()
            .AddUserStore<ApplicationUserStore>()
            .AddRoleStore<ApplicationRoleStore>()
            .AddUserStore<FrontOfficeApplicationUserStore>()
            .AddRoleStore<FrontOfficeApplicationRoleStore>()
            .AddClaimsPrincipalFactory<ApplicationUserClaimsPrincipalFactory>()
            .AddDefaultUI();

            services.Configure<IdentityOptions>(options =>
            {
                options.ClaimsIdentity.UserNameClaimType = Claims.Name;
                options.ClaimsIdentity.UserIdClaimType = Claims.Subject;
                options.ClaimsIdentity.RoleClaimType = Claims.Role;
                options.ClaimsIdentity.EmailClaimType = Claims.Email;
                options.SignIn.RequireConfirmedAccount = false;
            });

            return services;
        }

        public static IServiceCollection AddIdentityServer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(2);
                options.Cookie.HttpOnly = true;
                options.Cookie.SameSite = SameSiteMode.None;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            });

            services.AddAuthentication(OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme);

            var openIddictBuilder = services.AddOpenIddict();

            openIddictBuilder.AddCore(options =>
            {
                options.UseEntityFrameworkCore()
                       .UseDbContext<IdentityContext>();
            });

            openIddictBuilder.AddCore(options =>
            {
                options.UseEntityFrameworkCore()
                       .UseDbContext<FrontOfficeIdentityContext>();

            });

            openIddictBuilder.AddServer(options =>
            {
                var baseUrl = configuration.GetValue<string>("BaseUrl");
                if (!string.IsNullOrEmpty(baseUrl))
                {
                    options.SetIssuer(new Uri(baseUrl));
                }

                options.SetAuthorizationCodeLifetime(TimeSpan.FromHours(2));
                options.SetAccessTokenLifetime(TimeSpan.FromHours(1));
                options.SetIdentityTokenLifetime(TimeSpan.FromHours(6));
                options.SetRefreshTokenLifetime(TimeSpan.FromDays(14));
                options.SetUserCodeLifetime(TimeSpan.FromHours(4));

                options.SetAuthorizationEndpointUris("/connect/authorize")
                       .SetDeviceEndpointUris("/connect/device")
                       .SetLogoutEndpointUris("/connect/logout")
                       .SetIntrospectionEndpointUris("/connect/introspect")
                       .SetTokenEndpointUris("/connect/token")
                       .SetUserinfoEndpointUris("/connect/userinfo")
                       .SetVerificationEndpointUris("/connect/verify");

                options.AllowAuthorizationCodeFlow()
                       .AllowDeviceCodeFlow()
                       .AllowHybridFlow()
                       .AllowRefreshTokenFlow()
                       .AllowPasswordFlow();

                options.RegisterScopes(Scopes.Email, Scopes.Profile, Scopes.Roles,
                    AuthorizationConstants.Scopes.EduLink, AuthorizationConstants.Scopes.BackOffice, Scopes.OfflineAccess);

                options.AddEphemeralEncryptionKey().AddEphemeralSigningKey();
                options.DisableAccessTokenEncryption();

                options.RequireProofKeyForCodeExchange();

                options.UseAspNetCore()
                       .EnableStatusCodePagesIntegration()
                       .EnableAuthorizationEndpointPassthrough()
                       .EnableLogoutEndpointPassthrough()
                       .EnableTokenEndpointPassthrough()
                       .EnableUserinfoEndpointPassthrough()
                       .EnableVerificationEndpointPassthrough()
                       .DisableTransportSecurityRequirement();

                options.DisableScopeValidation();
            })
            .AddValidation(options =>
            {
                options.UseLocalServer();
                options.UseAspNetCore();
            });

            services.AddScoped<IOpenIddictTokenStore<OpenIddictEntityFrameworkCoreToken>, CustomTokenStore>();
            services.AddScoped<IOpenIddictApplicationStore<OpenIddictEntityFrameworkCoreApplication>, CustomApplicationStore>();
            services.AddScoped<IOpenIddictAuthorizationStore<OpenIddictEntityFrameworkCoreAuthorization>, CustomAuthorizationStore>();

            services.AddSingleton<IOpenIddictApplicationStoreResolver, CustomApplicationStoreResolver>();
            services.AddSingleton<IOpenIddictAuthorizationStoreResolver, CustomAuthorizationStoreResolver>();
            
            return services;
        }

    }

}
