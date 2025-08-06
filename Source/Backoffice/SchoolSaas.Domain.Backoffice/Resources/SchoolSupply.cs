using SchoolSaas.Domain.Common.Entities;

namespace SchoolSaas.Domain.Backoffice.Resources
{
    public class SchoolSupply : BaseEntity<Guid>
    {
        public string NameFr { get; set; }
        public string? NameAr { get; set; }
        public string? NameEn { get; set; }
        public string? DescriptionFr { get; set; }
        public string? DescriptionAr { get; set; }
        public string? DescriptionEn { get; set; }
    }

}
