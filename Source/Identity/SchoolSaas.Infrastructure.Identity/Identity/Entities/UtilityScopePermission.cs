using SchoolSaas.Domain.Common.Entities;

namespace SchoolSaas.Infrastructure.Identity.Identity.Entities
{
    public class UtilityScopePermission : BaseEntity<Guid>
    {
        public Guid UtilityScopeId { get; set; }
        public UtilityScope UtilityScope { get; set; }

        public Guid PermissionId { get; set; }
        public Permission Permission { get; set; }
    }
}
