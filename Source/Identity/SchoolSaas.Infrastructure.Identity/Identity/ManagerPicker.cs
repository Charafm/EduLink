using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OpenIddict.Core;
using OpenIddict.EntityFrameworkCore.Models;
using SchoolSaas.Infrastructure.Identity.Identity.Entities;

namespace SchoolSaas.Infrastructure.Identity.Identity
{
    public class ManagerPicker
    {
        private readonly IServiceProvider _serviceProvider;

        public ManagerPicker(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public UserManager<ApplicationUser> GetUserManager(DbContext context)
        {
            var userStore = new UserStore<ApplicationUser, ApplicationRole, DbContext, string, ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin, ApplicationUserToken, ApplicationRoleClaim>(context);

            // Resolve dependencies from _serviceProvider
            var options = _serviceProvider.GetRequiredService<IOptions<IdentityOptions>>();
            var passwordHasher = _serviceProvider.GetRequiredService<IPasswordHasher<ApplicationUser>>();
            var userValidators = _serviceProvider.GetServices<IUserValidator<ApplicationUser>>();
            var passwordValidators = _serviceProvider.GetServices<IPasswordValidator<ApplicationUser>>();
            var keyNormalizer = _serviceProvider.GetRequiredService<ILookupNormalizer>();
            var errorDescriber = _serviceProvider.GetRequiredService<IdentityErrorDescriber>();
            var logger = _serviceProvider.GetRequiredService<ILogger<UserManager<ApplicationUser>>>();

            return new UserManager<ApplicationUser>(
                userStore,
                options,
                passwordHasher,
                userValidators,
                passwordValidators,
                keyNormalizer,
                errorDescriber,
                _serviceProvider,
                logger);
        }

        public RoleManager<ApplicationRole> GetRoleManager(DbContext context)
        {
            var roleStore = new RoleStore<ApplicationRole, DbContext, string, ApplicationUserRole, ApplicationRoleClaim>(context);
            var options = _serviceProvider.GetRequiredService<IOptions<IdentityOptions>>();
            var roleValidators = _serviceProvider.GetServices<IRoleValidator<ApplicationRole>>();
            var keyNormalizer = _serviceProvider.GetRequiredService<ILookupNormalizer>();
            var errorDescriber = _serviceProvider.GetRequiredService<IdentityErrorDescriber>();
            var logger = _serviceProvider.GetRequiredService<ILogger<RoleManager<ApplicationRole>>>();

            return new RoleManager<ApplicationRole>(
                roleStore,
                roleValidators,
                keyNormalizer,
                errorDescriber,
                logger);
        }
        public SignInManager<ApplicationUser> GetSignInManager(UserManager<ApplicationUser> userManager)
        {
            return ActivatorUtilities.CreateInstance<SignInManager<ApplicationUser>>(_serviceProvider, userManager);
        }
        public OpenIddictTokenManager<OpenIddictEntityFrameworkCoreToken> GetTokenManager()
        {
            return _serviceProvider.GetRequiredService<OpenIddictTokenManager<OpenIddictEntityFrameworkCoreToken>>();
        }
        public OpenIddictApplicationManager<OpenIddictEntityFrameworkCoreApplication> GetApplicationManager()
        {
            return _serviceProvider.GetRequiredService<OpenIddictApplicationManager<OpenIddictEntityFrameworkCoreApplication>>();
        }



    }
}
