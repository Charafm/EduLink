using Microsoft.AspNetCore.Identity;

namespace SchoolSaas.Infrastructure.Identity.Identity.Entities
{
    public class ApplicationUserLogin : IdentityUserLogin<string>
    {
        public virtual ApplicationUser User { get; set; }
    }
}