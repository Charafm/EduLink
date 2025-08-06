using SchoolSaas.Domain.Common.Entities;

namespace SchoolSaas.Infrastructure.Identity.Identity.Entities
{
    public class UtilityScope : TitledEntity
    {
        public ICollection<UtilityScopePermission> UtilityScopePermissions { get; set; }
        public ICollection<RoleUtilityScope> RoleUtilityScopes { get; set; }
    }
}

