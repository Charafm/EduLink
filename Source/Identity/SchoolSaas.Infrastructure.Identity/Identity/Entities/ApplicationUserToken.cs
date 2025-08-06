using Microsoft.AspNetCore.Identity;

namespace SchoolSaas.Infrastructure.Identity.Identity.Entities
{
    public class ApplicationUserToken : IdentityUserToken<string>
    {
        public virtual ApplicationUser User { get; set; }
    }
}