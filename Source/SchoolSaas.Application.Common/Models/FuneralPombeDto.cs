namespace SchoolSaas.Application.Common.Models
{
    public class FuneralPombeDto : FuneralPombeUpdateDto
    {
        public string? CNIE { get; set; }
        public bool IsActive { get; set; }
        public string Password { get; set; }
        public string Ref { get; set; }
        public Guid PostRattachementId { get; set; } 
    }

    public class FuneralPombeResultDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string SocialReason { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string? TaxIdentification { get; set; } 
        public Guid UserId { get; set; }
        public Guid? PostRattachementId { get; set; }
        public Guid? CountryId { get; set; }
        public string? CountryTitle { get; set; }
        public bool IsActive { get; set; }
        public string Ref { get; set; }
        public string? PostRattachementTitles { get; set; }
    }

    public class FuneralPombeUpdateDto
    {
        public string? Name { get; set; }
        public string? SocialReason { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? EmailAddress { get; set; }
        public string? TaxIdentification { get; set; } // Optional
        public Guid? PostRattachementId { get; set; }
        public Guid? CountryId { get; set; }
        public bool? IsActive { get; set; }
    }

    public class FuneralPombeSearchDto
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string SocialReason { get; set; }
        public Guid? PostRattachementId { get; set; }
        public Guid? CountryId { get; set; }
    }

    
}
