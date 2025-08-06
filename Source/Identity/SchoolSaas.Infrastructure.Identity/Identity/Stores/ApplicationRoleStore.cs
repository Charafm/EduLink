using SchoolSaas.Infrastructure.Identity.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SchoolSaas.Infrastructure.Identity.Identity.Entities;

namespace SchoolSaas.Infrastructure.Identity.Identity.Stores
{
    public class ApplicationRoleStore : RoleStore<ApplicationRole, IdentityContext,
        string, ApplicationUserRole, ApplicationRoleClaim>
    {
        public ApplicationRoleStore(IdentityContext context, IdentityErrorDescriber describer = null)
            : base(context, describer)
        {
        }
    }
}