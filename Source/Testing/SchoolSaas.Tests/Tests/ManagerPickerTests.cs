using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Infrastructure.Identity.Identity;
using SchoolSaas.Infrastructure.Identity.Identity.Entities;
using SchoolSaas.Tests.Utilities;
using Xunit.Abstractions;

namespace SchoolSaas.Tests.Tests
{
    public class ManagerPickerTests : TestLoggerBase
    {
        public ManagerPickerTests(ITestOutputHelper output) : base(output) { }

        [Fact]
        public async Task GetUserManager_ForIdentityContext_ShouldReturnManagerAndAllowUserCreation()
        {
            var serviceProvider = TestServiceProviderFactory.CreateServiceProviderForIdentity();
            var contextFactory = serviceProvider.GetRequiredService<IContextFactory>();
            var managerPicker = serviceProvider.GetRequiredService<ManagerPicker>();

            string clientId = "m2m.SchoolSaas.Backoffice";
            var dbContext = contextFactory.CreateContext(clientId);
            var userManager = managerPicker.GetUserManager(dbContext);

            var user = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "testuser@identity.com",
                Email = "testuser@identity.com",
                TenantId = "TestTenant"
            };

            var result = await userManager.CreateAsync(user, "Test@123");
            Assert.True(result.Succeeded, "User creation should succeed");

            //var fetchedUser = await userManager.FindByNameAsync("testuser@identity.com");
            var fetchedUser =  dbContext.Set<ApplicationUser>().AsNoTracking().FirstOrDefaultAsync(u => u.UserName == user.UserName).Result;
            Assert.NotNull(fetchedUser);
            Log("✅ User creation succeeded in IdentityContext");
        }
        [Fact]
        public async Task GetUserManager_ForFOIdentityContext_ShouldReturnManagerAndAllowUserCreation()
        {
            var serviceProvider = TestServiceProviderFactory.CreateServiceProviderForIdentity();
            var contextFactory = serviceProvider.GetRequiredService<IContextFactory>();
            var managerPicker = serviceProvider.GetRequiredService<ManagerPicker>();

            string clientId = "m2m.SchoolSaas.Frontoffice";
            var dbContext = contextFactory.CreateContext(clientId);
            var userManager = managerPicker.GetUserManager(dbContext);

            var user = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "testuser@identity.com",
                Email = "testuser@identity.com",
                TenantId = "TestTenant"
            };

            var result = await userManager.CreateAsync(user, "Test@123");
            Assert.True(result.Succeeded, "User creation should succeed");

            var fetchedUser = dbContext.Set<ApplicationUser>().AsNoTracking().FirstOrDefaultAsync(u => u.UserName == user.UserName).Result;
            Assert.NotNull(fetchedUser);
            Log("✅ User creation succeeded in IdentityContext");
        }
    }
}