using SchoolSaas.Domain.Common.Enums;

namespace SchoolSaas.Application.Common.Models
{
    public class CreateUserRequestDto
    {
        public string? UserId { get; set; }
        public Guid? BranchId { get; set; }
        public string FirstNameFr { get; set; }
        public string FirstNameAr { get; set; }
        public string LastNameFr { get; set; }
        public string LastNameAr { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string? CIN { get; set; }
        public string? Occupation { get; set; }
        public string? AddressFr { get; set; }
        public string? AddressAr { get; set; }

        public string Password { get; set; }
 
        public UserType type { get; set; }
    }
    public class CreateStudentUserDTO
    {
        public Guid? BranchId { get; set; }
        public string FirstNameFr { get; set; }
        public string FirstNameAr { get; set; }
        public string LastNameFr { get; set; }
        public string LastNameAr { get; set; }
        public string Email { get; set; }
        public string? Phone { get; set; }
        public string? AddressFr { get; set; }
        public string? AddressAr { get; set; }
        public string Password { get; set; }
     
    }

}
