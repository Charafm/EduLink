using SchoolSaas.Domain.Common.Entities;

namespace SchoolSaas.Infrastructure.Identity.Identity.Entities
{ 
    public class Permission : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public ICollection<UserPermission> UserPermissions { get; set;}
    }
}
