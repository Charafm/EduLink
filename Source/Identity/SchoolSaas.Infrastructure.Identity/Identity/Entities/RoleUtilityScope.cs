using SchoolSaas.Domain.Common.Entities;

namespace SchoolSaas.Infrastructure.Identity.Identity.Entities
{
    public class RoleUtilityScope : BaseEntity<Guid>
    {
        public string RoleId { get; set; }
        public ApplicationRole Role { get; set; }

        public Guid UtilityScopeId { get; set; }
        public UtilityScope UtilityScope { get; set; }
    }
}
