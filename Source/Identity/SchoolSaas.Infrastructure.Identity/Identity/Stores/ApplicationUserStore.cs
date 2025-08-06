using SchoolSaas.Infrastructure.Identity.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolSaas.Infrastructure.Identity.Identity.Entities;

namespace SchoolSaas.Infrastructure.Identity.Identity.Stores
{
    public class ApplicationUserStore : UserStore<ApplicationUser, ApplicationRole,
        IdentityContext, string, ApplicationUserClaim, ApplicationUserRole,
        ApplicationUserLogin, ApplicationUserToken, ApplicationRoleClaim>, IQueryableUserStore<ApplicationUser>
    {
        public ApplicationUserStore(IdentityContext context, IdentityErrorDescriber? describer = null) : base(
            context,
            describer)
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
        public async  override Task<ApplicationUser> FindByEmailAsync(string email,
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