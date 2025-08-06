using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using SchoolSaas.Infrastructure.Identity.Identity.Entities;
using SchoolSaas.Infrastructure.Identity.Context;

namespace SchoolSaas.Infrastructure.Identity.Identity.Stores
{
    public class FrontOfficeApplicationRoleStore : RoleStore<ApplicationRole, FrontOfficeIdentityContext,
     string, ApplicationUserRole, ApplicationRoleClaim>
    {
        public FrontOfficeApplicationRoleStore(FrontOfficeIdentityContext context, IdentityErrorDescriber describer = null)
            : base(context, describer)
        {
        }
    }
}
