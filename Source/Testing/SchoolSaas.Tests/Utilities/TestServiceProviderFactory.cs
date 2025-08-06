using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OpenIddict.Core;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Infrastructure.Identity.Context;
using SchoolSaas.Infrastructure.Identity.Identity.Entities;
using SchoolSaas.Infrastructure.Identity.Identity.Stores;
using SchoolSaas.Infrastructure.Identity.Identity;
using Moq;
using OpenIddict.EntityFrameworkCore;
using OpenIddict.Abstractions;
using OpenIddict.EntityFrameworkCore.Models;

namespace SchoolSaas.Tests.Utilities
{
    public static class TestServiceProviderFactory
    {
        public static IServiceProvider CreateServiceProviderForIdentity()
        {
            var services = new ServiceCollection();

            // ✅ Essential dependencies
            services.AddOptions();
            services.AddLogging();
            services.AddMemoryCache();
            services.AddSingleton<ICurrentUserService>(Mock.Of<ICurrentUserService>());
            services.AddSingleton<IDateTime>(Mock.Of<IDateTime>());
            services.AddSingleton<ITenantAccessor>(new FakeTenantAccessor("TestTenant"));
            services.AddDataProtection();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // ✅ Register both databases
            services.AddDbContext<IdentityContext>(options =>
            {
                options.UseInMemoryDatabase("IdentityTestDb")
                       .EnableSensitiveDataLogging();
                options.UseOpenIddict();
            });

            services.AddDbContext<FrontOfficeIdentityContext>(options =>
            {
                options.UseInMemoryDatabase("FrontOfficeTestDb")
                       .EnableSensitiveDataLogging();
                options.UseOpenIddict();
            });

            // ✅ Identity configuration
            services.AddIdentityCore<ApplicationUser>(options =>
            {
                options.User.RequireUniqueEmail = true;
            })
            .AddRoles<ApplicationRole>()
            .AddEntityFrameworkStores<IdentityContext>()
            .AddDefaultTokenProviders();

            // ✅ OpenIddict configuration
            services.Configure<OpenIddictCoreOptions>(options => { });
            services.AddSingleton<IOptionsMonitor<OpenIddictCoreOptions>, OptionsMonitorStub<OpenIddictCoreOptions>>();
            services.Configure<OpenIddictEntityFrameworkCoreOptions>(options => { });
            services.AddSingleton<IOptionsMonitor<OpenIddictEntityFrameworkCoreOptions>, OptionsMonitorStub<OpenIddictEntityFrameworkCoreOptions>>();

            // ✅ Register custom stores, resolvers, and OpenIddict services
            services.AddScoped<IOpenIddictApplicationStore<OpenIddictEntityFrameworkCoreApplication>, CustomApplicationStore>();
            services.AddScoped<IOpenIddictApplicationStoreResolver, CustomApplicationStoreResolver>();
            services.AddScoped<IOpenIddictTokenStore<OpenIddictEntityFrameworkCoreToken>, CustomTokenStore>();
            services.AddScoped<IOpenIddictTokenStoreResolver, CustomTokenStoreResolver>();
            services.AddScoped<IOpenIddictAuthorizationStore<OpenIddictEntityFrameworkCoreAuthorization>, CustomAuthorizationStore>();
            services.AddScoped<IOpenIddictAuthorizationStoreResolver, CustomAuthorizationStoreResolver>();

            // ✅ Add OpenIddict Application Manager
            services.AddScoped<OpenIddictApplicationManager<OpenIddictEntityFrameworkCoreApplication>>();

            // ✅ Fix: Ensure OpenIddict cache is available
            services.AddSingleton<IOpenIddictApplicationCache<OpenIddictEntityFrameworkCoreApplication>, DummyApplicationCache>();

            // ✅ Ensure ManagerPicker is properly registered
            services.AddSingleton<IContextFactory, TestContextFactory>();
            services.AddSingleton<ManagerPicker>();

            return services.BuildServiceProvider();
        }
    }

    // Stub for IOptionsMonitor<T>
    public class OptionsMonitorStub<T> : IOptionsMonitor<T> where T : class, new()
    {
        public T CurrentValue { get; } = new T();
        public T Get(string name) => CurrentValue;
        public IDisposable OnChange(Action<T, string> listener) => null;
    }

    // Fake TenantAccessor.
    public class FakeTenantAccessor : ITenantAccessor
    {
        private readonly string _tenantId;
        public FakeTenantAccessor(string tenantId) { _tenantId = tenantId; }
        public string GetTenantId() => _tenantId;
    }
}
