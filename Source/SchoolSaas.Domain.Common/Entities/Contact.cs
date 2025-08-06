using SchoolSaas.Domain.Common.Enums;

namespace SchoolSaas.Domain.Common.Entities
{
    public class Contact : TitledEntity
    {

        public Guid? CountryTaxId { get; set; }
        public ProfileType? Profile { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public string? AddressLine2 { get; set; }
        public string? ZipCode { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public Guid? CityId { get; set; }

    }
}
