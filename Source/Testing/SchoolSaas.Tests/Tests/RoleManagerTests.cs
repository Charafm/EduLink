using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Infrastructure.Identity.Identity.Entities;
using SchoolSaas.Infrastructure.Identity.Identity;
using SchoolSaas.Tests.Utilities;
using Xunit.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace SchoolSaas.Tests.Tests
{
    public class RoleManagerTests : TestLoggerBase
    {
        public RoleManagerTests(ITestOutputHelper output) : base(output) { }

        [Theory]
        [InlineData("m2m.SchoolSaas.Backoffice")]
        [InlineData("m2m.SchoolSaas.Front")]
        public async Task CreateRole_ShouldPersistCorrectly(string databaseContext)
        {
            var serviceProvider = TestServiceProviderFactory.CreateServiceProviderForIdentity();
            var contextFactory = serviceProvider.GetRequiredService<IContextFactory>();
            var managerPicker = serviceProvider.GetRequiredService<ManagerPicker>();

            var dbContext = contextFactory.CreateContext(databaseContext);
            var roleManager = managerPicker.GetRoleManager(dbContext);

            var role = new ApplicationRole
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Admin"
            };

            var result = await roleManager.CreateAsync(role);
            Assert.True(result.Succeeded, "Role creation should succeed");

            var fetchedRole = await dbContext.Set<ApplicationRole>().FindAsync(role.Id);
            Assert.NotNull(fetchedRole);
            Log($"✅ Role successfully created and verified in {databaseContext}.");
        }

        [Theory]
        [InlineData("m2m.SchoolSaas.Backoffice")]
        [InlineData("m2m.SchoolSaas.Front")]
        public async Task DeleteRole_ShouldRemoveCorrectly(string databaseContext)
        {
            var serviceProvider = TestServiceProviderFactory.CreateServiceProviderForIdentity();
            var contextFactory = serviceProvider.GetRequiredService<IContextFactory>();
            var managerPicker = serviceProvider.GetRequiredService<ManagerPicker>();

            var dbContext = contextFactory.CreateContext(databaseContext);
            var roleManager = managerPicker.GetRoleManager(dbContext);

            var role = new ApplicationRole
            {
                Id = Guid.NewGuid().ToString(),
                Name = "TestRole"
            };

            await roleManager.CreateAsync(role);
            await roleManager.DeleteAsync(role);

            var fetchedRole = await dbContext.Set<ApplicationRole>().FindAsync(role.Id);
            Assert.Null(fetchedRole);
            Log($"✅ Role successfully deleted from {databaseContext}.");
        }

        [Theory]
        [InlineData("m2m.SchoolSaas.Backoffice")]
        [InlineData("m2m.SchoolSaas.Front")]
        public async Task FindRole_ById_ByName_ShouldReturnCorrectRole(string databaseContext)
        {
            var serviceProvider = TestServiceProviderFactory.CreateServiceProviderForIdentity();
            var contextFactory = serviceProvider.GetRequiredService<IContextFactory>();
            var managerPicker = serviceProvider.GetRequiredService<ManagerPicker>();

            var dbContext = contextFactory.CreateContext(databaseContext);
            var roleManager = managerPicker.GetRoleManager(dbContext);

            var role = new ApplicationRole
            {
                Id = Guid.NewGuid().ToString(),
                Name = "TestRole"
            };

            await roleManager.CreateAsync(role);

            // Find by ID
            var fetchedById = await roleManager.FindByIdAsync(role.Id);
            Assert.NotNull(fetchedById);
            Assert.Equal(role.Name, fetchedById.Name);

            // Find by Name
            var fetchedByName = await roleManager.FindByNameAsync(role.Name);
            Assert.NotNull(fetchedByName);
            Assert.Equal(role.Id, fetchedByName.Id);

            Log($"✅ Role found by ID and Name in {databaseContext}.");
        }
    }
}
