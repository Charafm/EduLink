using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using OpenIddict.EntityFrameworkCore.Models;
using OpenIddict.EntityFrameworkCore;
using SchoolSaas.Infrastructure.Identity.Identity.Stores;
using SchoolSaas.Tests.Utilities;
using Microsoft.Extensions.DependencyInjection;
using SchoolSaas.Infrastructure.Identity.Context;
using Xunit.Abstractions;

namespace SchoolSaas.Tests.Tests
{
    public class CustomApplicationStoreTests : TestLoggerBase
    {
        public CustomApplicationStoreTests(ITestOutputHelper output) : base(output) { }

        private CustomApplicationStore CreateApplicationStore(IServiceProvider serviceProvider)
        {
            var frontOfficeContext = serviceProvider.GetRequiredService<FrontOfficeIdentityContext>();
            var identityContext = serviceProvider.GetRequiredService<IdentityContext>();
            var cache = serviceProvider.GetRequiredService<IMemoryCache>();
            var options = serviceProvider.GetRequiredService<IOptionsMonitor<OpenIddictEntityFrameworkCoreOptions>>();

            return new CustomApplicationStore(frontOfficeContext, identityContext, cache, options, serviceProvider);
        }

        [Fact]
        public async Task CreateApplication_ShouldPersistCorrectly()
        {
            var serviceProvider = TestServiceProviderFactory.CreateServiceProviderForIdentity();
            var store = CreateApplicationStore(serviceProvider);

            var application = new OpenIddictEntityFrameworkCoreApplication
            {
                ClientId = "test-client",
                DisplayName = "Test Application"
            };

            await store.CreateAsync(application, CancellationToken.None);

            var foundApplication = await store.FindByIdAsync(application.Id!, CancellationToken.None);
            Assert.NotNull(foundApplication);
            Assert.Equal("Test Application", foundApplication.DisplayName);
            Log($"✅ Application successfully created and verified.");
        }

        [Fact]
        public async Task FindApplication_ById_ShouldReturnCorrectApplication()
        {
            var serviceProvider = TestServiceProviderFactory.CreateServiceProviderForIdentity();
            var store = CreateApplicationStore(serviceProvider);

            var application = new OpenIddictEntityFrameworkCoreApplication
            {
                ClientId = "find-client",
                DisplayName = "Find App"
            };

            await store.CreateAsync(application, CancellationToken.None);

            var fetchedById = await store.FindByIdAsync(application.Id!, CancellationToken.None);
            Assert.NotNull(fetchedById);
            Assert.Equal("Find App", fetchedById.DisplayName);
            Log($"✅ Application found by ID.");
        }

        [Fact]
        public async Task UpdateApplication_ShouldModifyDataCorrectly()
        {
            var serviceProvider = TestServiceProviderFactory.CreateServiceProviderForIdentity();
            var store = CreateApplicationStore(serviceProvider);

            var application = new OpenIddictEntityFrameworkCoreApplication
            {
                ClientId = "update-client",
                DisplayName = "Old Name"
            };

            await store.CreateAsync(application, CancellationToken.None);

            application.DisplayName = "Updated Name";
            await store.UpdateAsync(application, CancellationToken.None);

            var updatedApplication = await store.FindByIdAsync(application.Id!, CancellationToken.None);
            Assert.NotNull(updatedApplication);
            Assert.Equal("Updated Name", updatedApplication.DisplayName);
            Log($"✅ Application updated successfully.");
        }

        [Fact]
        public async Task DeleteApplication_ShouldRemoveCorrectly()
        {
            var serviceProvider = TestServiceProviderFactory.CreateServiceProviderForIdentity();
            var store = CreateApplicationStore(serviceProvider);

            var application = new OpenIddictEntityFrameworkCoreApplication
            {
                ClientId = "delete-client",
                DisplayName = "To Delete"
            };

            await store.CreateAsync(application, CancellationToken.None);
            await store.DeleteAsync(application, CancellationToken.None);

            var foundApplication = await store.FindByIdAsync(application.Id!, CancellationToken.None);
            Assert.Null(foundApplication);
            Log($"✅ Application deleted successfully.");
        }
    }


}

