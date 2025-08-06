    using SchoolSaas.Domain.Common.Entities;

namespace SchoolSaas.Domain.Backoffice.Students
{
    public class ParentDetail : BaseEntity<Guid>
    {
        public Guid ParentId { get; set; }

        public string AddressFr { get; set; }
        public string? AddressAr { get; set; }
        public string? Occupation { get; set; }

        public string? AdditionalContactInfo { get; set; } 

        public Parent Parent { get; set; }
    }
}