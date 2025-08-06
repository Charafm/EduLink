using SchoolSaas.Domain.Backoffice.School;
using SchoolSaas.Domain.Common.Entities;

namespace SchoolSaas.Domain.Backoffice.Students
{
    public class Parent : BaseEntity<Guid>
    {
        public string UserId { get; set; }
    
        public string FirstNameFr { get; set; }
        public string FirstNameAr { get; set; }
        public string LastNameFr { get; set; }
        public string LastNameAr { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string CIN { get; set; }
        public string Occupation { get; set; }
        public string AddressFr { get; set; }
        public string AddressAr { get; set; }
        public bool IsIdentityVerified { get; set; }
        public DateTime? VerificationDate { get; set; }
        public bool IsDeleted { get; set; }
    
        public ParentDetail? ParentDetail { get; set; }
    }
}
