using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using SchoolSaas.Infrastructure.Identity.Identity.Entities;
using System.Security.Claims;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace SchoolSaas.Infrastructure.Identity.Identity
{
    public class ApplicationUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser>
    {
        RoleManager<ApplicationRole> _roleManager;
        public ApplicationUserClaimsPrincipalFactory(
            UserManager<ApplicationUser> userManager,
            IOptions<IdentityOptions> optionsAccessor, RoleManager<ApplicationRole> roleManager)
            : base(userManager, optionsAccessor)
        {
            _roleManager = roleManager;
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);

            List<Claim> claims = new List<Claim>();

            claims.Add(new Claim(Claims.PreferredUsername, $"{user.LastName} {user.FirstName}"));

            if (UserManager.SupportsUserRole)
            {
                IList<string> roles = await UserManager.GetRolesAsync(user);
                foreach (var roleName in roles)
                {
                    claims.Add(new Claim(Claims.Role, roleName));
                    if (_roleManager.SupportsRoleClaims)
                    {
                        ApplicationRole role = await _roleManager.FindByNameAsync(roleName);
                        if (role != null)
                        {
                            claims.AddRange(await _roleManager.GetClaimsAsync(role));
                        }
                    }
                }
            }

            identity.AddClaims(claims);

            return identity;
        }
    }
}