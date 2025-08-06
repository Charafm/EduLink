using Microsoft.AspNetCore.Identity;

namespace SchoolSaas.Infrastructure.Identity.Identity.Entities
{
    public class ApplicationUserClaim : IdentityUserClaim<string>
    {
        public virtual ApplicationUser User { get; set; }
    }
}