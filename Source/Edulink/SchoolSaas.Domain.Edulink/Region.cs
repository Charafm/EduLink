using SchoolSaas.Domain.Common.Entities;

namespace SchoolSaas.Domain.Edulink
{
    public class Region : BaseEntity<Guid>
    {
        public string NameFr { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }

        public ICollection<City> Cities { get; set; } = new List<City>();
    }
}
