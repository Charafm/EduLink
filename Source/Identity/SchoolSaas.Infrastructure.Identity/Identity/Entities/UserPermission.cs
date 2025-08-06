using SchoolSaas.Domain.Common.Entities;

namespace SchoolSaas.Infrastructure.Identity.Identity.Entities
{
    public class UserPermission : BaseEntity<Guid>
    {
        public string UserId { get; set; }
        public Guid PermissionId { get; set; }
        public ApplicationUser User { get; set; }
        public Permission Permission { get; set; }
        
  
    }
}
