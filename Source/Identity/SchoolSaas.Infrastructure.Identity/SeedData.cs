using SchoolSaas.Domain.Common.Constants;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SchoolSaas.Infrastructure.Identity.Context;
using SchoolSaas.Infrastructure.Identity.Identity;
using SchoolSaas.Infrastructure.Identity.Identity.Entities;
using Microsoft.EntityFrameworkCore;
using OpenIddict.EntityFrameworkCore.Models;
using System.Text;
using System.Security.Cryptography;

namespace SchoolSaas.Infrastructure.Identity
{
    public class SeedData
    {
        public static void EnsureSeedData(WebApplication app)
        {
            using (var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var identityContext = scope.ServiceProvider.GetService<IdentityContext>();
                var frontOfficeIdentityContext = scope.ServiceProvider.GetService<FrontOfficeIdentityContext>();
                var managerPicker = scope.ServiceProvider.GetRequiredService<ManagerPicker>();

                if (identityContext != null)
                {
                    SeedDatabase(identityContext, managerPicker);
                }

                if (frontOfficeIdentityContext != null)
                {
                    SeedDatabase(frontOfficeIdentityContext, managerPicker);
                }
            }
        }

        private static void SeedDatabase(DbContext context, ManagerPicker managerPicker)
        {
            context.Database.EnsureCreated();

            var userMgr = managerPicker.GetUserManager(context);
            var roleMgr = managerPicker.GetRoleManager(context);

            SeedRoles(roleMgr);
            SeedUsers(userMgr, context);
            SeedOpenIddictData(context, managerPicker);
        }

        private static void SeedRoles(RoleManager<ApplicationRole> roleMgr)
        {
            foreach (var roleField in typeof(AuthorizationConstants.Roles).GetFields())
            {
                var roleName = (string?)roleField.GetValue(null);
                if (!roleMgr.RoleExistsAsync(roleName).Result)
                {
                    roleMgr.CreateAsync(new ApplicationRole
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = roleName
                    }).Wait();
                }
            }
        }

        private static void SeedUsers(UserManager<ApplicationUser> userMgr, DbContext context)
        {
            var superAdminRole = AuthorizationConstants.Roles.SuperAdmins;
            var username = "SuperAdmin";
            var userEmail = "SuperAdminUser@EduLink.ma";
            var userNameNormalized = ($"{username.ToLower()}@EduLink").ToUpper();

            // Direct query to check if the user exists in the DB
            var dbUser = context.Set<ApplicationUser>().IgnoreQueryFilters()
                .FirstOrDefault(u => u.NormalizedUserName == userNameNormalized);

            if (dbUser != null)
            {
                Console.WriteLine($"User {userNameNormalized} already exists in DB but not found by UserManager.");
                return; // Skip creation
            }

            var existingUser = userMgr.FindByNameAsync(userNameNormalized).Result;

            if (existingUser == null)
            {
                var user = new ApplicationUser
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = userNameNormalized,
                    NormalizedUserName = userNameNormalized.ToUpper(),
                    Email = userEmail,
                    NormalizedEmail = userEmail.ToUpper(),
                    FirstName = username,
                    LastName = "EduLink",
                    TenantId = GetDefaultTenantId(context)
                };

                var result = userMgr.CreateAsync(user, AuthorizationConstants.DefaultPassword).Result;

                if (result.Succeeded)
                {
                    user = userMgr.FindByNameAsync(user.UserName).Result;
                    if (user != null)
                    {
                        var roleResult = userMgr.AddToRoleAsync(user, superAdminRole).Result;
                        if (!roleResult.Succeeded)
                        {
                            throw new Exception($"Failed to assign role: {string.Join(", ", roleResult.Errors.Select(e => e.Description))}");
                        }
                    }
                    else
                    {
                        throw new Exception("User creation succeeded, but retrieval failed.");
                    }
                }
                else
                {
                    throw new Exception($"User creation failed: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }
        }

        private static void SeedOpenIddictData(DbContext context, ManagerPicker managerPicker)
        {
            // Use direct EF Core queries to check existing applications.
            var applications = context.Set<OpenIddictEntityFrameworkCoreApplication>();
            var hasher = new PasswordHasher<object>();
            foreach (var client in Config.GetClientDescriptors())
            {
                var existingClient = applications
                    .AsNoTracking()
                    .FirstOrDefault(a => a.ClientId == client.ClientId);

                if (existingClient == null)
                {
                    // Create a new application entity.
                    var newApp = new OpenIddictEntityFrameworkCoreApplication
                    {
                        Id = Guid.NewGuid().ToString(),
                        ClientId = client.ClientId,
                        // Hash the client secret using SHA256 before saving.
                        ClientSecret = client.ClientSecret != null ? hasher.HashPassword(null, client.ClientSecret) : null,
                        DisplayName = client.DisplayName,
                        RedirectUris = client.RedirectUris != null ? System.Text.Json.JsonSerializer.Serialize(client.RedirectUris) : null,
                        PostLogoutRedirectUris = client.PostLogoutRedirectUris != null ? System.Text.Json.JsonSerializer.Serialize(client.PostLogoutRedirectUris) : null,
                        Permissions = client.Permissions != null ? System.Text.Json.JsonSerializer.Serialize(client.Permissions) : null,

                    };

                    applications.Add(newApp);
                }
            }

            context.SaveChanges();
        }

        // Helper method to compute SHA256 hash.
        private static string ComputeSha256Hash(string rawData)
        {
            using (var sha256 = SHA256.Create())
            {
                // ComputeHash returns byte array.
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string.
                var builder = new StringBuilder();
                foreach (var b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        //private static void SeedOpenIddictData(DbContext context)
        //{
        //    // Get the DbSet for OpenIddict applications.
        //    var applications = context.Set<OpenIddictEntityFrameworkCoreApplication>();

        //    foreach (var client in Config.GetClientDescriptors())
        //    {
        //        // Check directly for an existing application with the same ClientId
        //        var existingClient = applications
        //            .AsNoTracking()
        //            .FirstOrDefault(a => a.ClientId == client.ClientId);

        //        if (existingClient == null)
        //        {
        //            // Create a new OpenIddict application entity.
        //            // You might need to adjust property mapping based on your Config type.
        //            var newApp = new OpenIddictEntityFrameworkCoreApplication
        //            {
        //                Id = Guid.NewGuid().ToString(),
        //                ClientId = client.ClientId,
        //                ClientSecret = client.ClientSecret,
        //                DisplayName = client.DisplayName,
        //                // Assuming that these properties are stored as semicolon-separated values
        //                RedirectUris = client.RedirectUris != null ? string.Join(";", client.RedirectUris) : null,
        //                PostLogoutRedirectUris = client.PostLogoutRedirectUris != null ? string.Join(";", client.PostLogoutRedirectUris) : null,
        //                Permissions = client.Permissions != null ? string.Join(";", client.Permissions) : null,
        //                // Add additional mappings as needed
        //            };

        //            applications.Add(newApp);
        //        }
        //    }

        //    context.SaveChanges();
        //}
        private static string? GetDefaultTenantId(DbContext context)
        {
            // Implement logic to retrieve a default TenantId if needed
            // Can be fetched from config or determined dynamically
            return null;
        }
    }

}
