using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolSaas.Infrastructure.Identity.Identity.Entities;
using SchoolSaas.Infrastructure.Identity.Context;

namespace SchoolSaas.Infrastructure.Identity.Identity.Stores
{
    public class FrontOfficeApplicationUserStore : UserStore<ApplicationUser, ApplicationRole,
     FrontOfficeIdentityContext, string, ApplicationUserClaim, ApplicationUserRole,
     ApplicationUserLogin, ApplicationUserToken, ApplicationRoleClaim>, IQueryableUserStore<ApplicationUser>
    {
        public FrontOfficeApplicationUserStore(FrontOfficeIdentityContext context, IdentityErrorDescriber? describer = null)
            : base(context, describer)
        {

        }
        public override async Task<ApplicationUser> FindByIdAsync(string userId, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            return await Users.IgnoreQueryFilters()
                .Include(e => e.Claims)
                .Include(e => e.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
        }

        public override async Task<ApplicationUser> FindByNameAsync(string normalizedUserName,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            return await Users.IgnoreQueryFilters()
                .Include(e => e.Claims)
                .Include(e => e.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.NormalizedUserName == normalizedUserName, cancellationToken);
        }
        public async override Task<ApplicationUser> FindByEmailAsync(string email,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            return await Users.IgnoreQueryFilters()
                .Include(e => e.Claims)
                .Include(e => e.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.NormalizedEmail == email, cancellationToken);
        }
    }
}
