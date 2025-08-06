using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Infrastructure.Identity.Identity.Entities;
using SchoolSaas.Infrastructure.Identity.Identity;
using SchoolSaas.Tests.Utilities;
using Xunit.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace SchoolSaas.Tests.Tests
{
    public class UserManagerTests : TestLoggerBase
    {
        public UserManagerTests(ITestOutputHelper output) : base(output) { }

        [Theory]
        [InlineData("m2m.SchoolSaas.Backoffice")]
        [InlineData("m2m.SchoolSaas.Front")]
        public async Task CreateUser_ShouldPersistCorrectly(string databaseContext)
        {
            var serviceProvider = TestServiceProviderFactory.CreateServiceProviderForIdentity();
            var contextFactory = serviceProvider.GetRequiredService<IContextFactory>();
            var managerPicker = serviceProvider.GetRequiredService<ManagerPicker>();

            var dbContext = contextFactory.CreateContext(databaseContext);
            var userManager = managerPicker.GetUserManager(dbContext);

            var user = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = $"testuser@{databaseContext}.com",
                Email = $"testuser@{databaseContext}.com",
                TenantId = "TestTenant"
            };

            var result = await userManager.CreateAsync(user, "Test@123");
            Assert.True(result.Succeeded, "User creation should succeed");

            var fetchedUser = await dbContext.Set<ApplicationUser>().FindAsync(user.Id);
            Assert.NotNull(fetchedUser);
            Log($"✅ User successfully created and verified in {databaseContext}.");
        }

        [Theory]
        [InlineData("m2m.SchoolSaas.Backoffice")]
        [InlineData("m2m.SchoolSaas.Front")]
        public async Task DeleteUser_ShouldRemoveCorrectly(string databaseContext)
        {
            var serviceProvider = TestServiceProviderFactory.CreateServiceProviderForIdentity();
            var contextFactory = serviceProvider.GetRequiredService<IContextFactory>();
            var managerPicker = serviceProvider.GetRequiredService<ManagerPicker>();

            var dbContext = contextFactory.CreateContext(databaseContext);
            var userManager = managerPicker.GetUserManager(dbContext);

            var user = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = $"deleteuser@{databaseContext}.com",
                Email = $"deleteuser@{databaseContext}.com",
                TenantId = "TestTenant"
            };

            await userManager.CreateAsync(user, "Test@123");
            await userManager.DeleteAsync(user);

            var fetchedUser = await dbContext.Set<ApplicationUser>().FindAsync(user.Id);
            Assert.Null(fetchedUser);
            Log($"✅ User successfully deleted from {databaseContext}.");
        }
        [Theory]
        [InlineData("m2m.SchoolSaas.Backoffice")]
        [InlineData("m2m.SchoolSaas.Front")]
        public async Task FindUser_ById_ByEmail_ShouldReturnCorrectUser(string databaseContext)
        {
            var serviceProvider = TestServiceProviderFactory.CreateServiceProviderForIdentity();
            var contextFactory = serviceProvider.GetRequiredService<IContextFactory>();
            var managerPicker = serviceProvider.GetRequiredService<ManagerPicker>();

            var dbContext = contextFactory.CreateContext(databaseContext);
            var userManager = managerPicker.GetUserManager(dbContext);

            var user = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = $"finduser@{databaseContext}.com",
                Email = $"finduser@{databaseContext}.com",
                TenantId = "TestTenant"
            };

            await userManager.CreateAsync(user, "Test@123");

            // Find by ID
            var fetchedById = await userManager.FindByIdAsync(user.Id);
            Assert.NotNull(fetchedById);
            Assert.Equal(user.Email, fetchedById.Email);

            // Find by Email
            var fetchedByEmail = await userManager.FindByEmailAsync(user.Email);
            Assert.NotNull(fetchedByEmail);
            Assert.Equal(user.Id, fetchedByEmail.Id);

            Log($"✅ User found by ID and Email in {databaseContext}.");
        }
    }
}
