using SchoolSaas.Application.Common.Mappings;
using SchoolSaas.Domain.Common.Entities;
using Microsoft.AspNetCore.Identity;
using SchoolSaas.Domain.Common.DataObjects.Common;

namespace SchoolSaas.Infrastructure.Identity.Identity.Entities
{
    public class ApplicationUser : IdentityUser<string>, IMapTo<UserDto>, IMapFrom<UserDto>,
        IAuditableEntity<string>, ITenantAware, IDeletableEntity
    {
        public string? TenantId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public virtual ICollection<ApplicationUserClaim> Claims { get; set; }
        public virtual ICollection<ApplicationUserLogin> Logins { get; set; }
        public virtual ICollection<ApplicationUserToken> Tokens { get; set; }
        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
        public ICollection<UserPermission> UserPermissions { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime Created { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModified { get; set; }

        public bool? IsDeleted { get; set; }
    }
}