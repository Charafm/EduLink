using SchoolSaas.Domain.Common.Entities;

namespace SchoolSaas.Domain.Backoffice.School
{
    public class Branch : BaseEntity<Guid>
    {
        public Guid SchoolId { get; set; }
        public string BranchNameFr { get; set; } = string.Empty;
        public string BranchNameAr { get; set; } = string.Empty;
        public string? BranchNameEn { get; set; } = string.Empty;
        public string AddressFr { get; set; } = string.Empty;
        public string AddressAr { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string PrincipalNameFr { get; set; } = string.Empty;
        public string PrincipalNameAr { get; set; } = string.Empty;
        public Guid CityId { get; set; }
    }
}
