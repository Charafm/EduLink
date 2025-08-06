using SchoolSaas.Domain.Common.Entities;

namespace SchoolSaas.Domain.Backoffice.Resources
{
    public class Book : BaseEntity<Guid>
    {
        public string TitleFr { get; set; }
        public string? TitleAr { get; set; }
        public string? TitleEn { get; set; }
        public string AuthorNameFr { get; set; }
        public string? AuthorNameAr { get; set; }
        public string? AuthorNameEn { get; set; }
        public string? ISBN { get; set; }
        public string Subject { get; set; }
    }

}
