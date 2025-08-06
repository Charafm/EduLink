using SchoolSaas.Domain.Common.Entities;
using Microsoft.AspNetCore.Identity;

namespace SchoolSaas.Infrastructure.Identity.Identity.Entities
{
    public class ApplicationRoleClaim : IdentityRoleClaim<string>, ITenantAware
    {
        public string? TenantId { get; set; }

        public virtual ApplicationRole Role { get; set; }
    }
}