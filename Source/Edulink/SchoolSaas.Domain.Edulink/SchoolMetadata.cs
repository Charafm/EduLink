using SchoolSaas.Domain.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Domain.Edulink
{
    public class SchoolMetadata : BaseEntity<Guid>
    {
        // Localized school name
        public string NameFr { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }

        public string Code { get; set; }

        // Localized address
        public string AddressFr { get; set; }
        public string AddressAr { get; set; }
        public string AddressEn { get; set; }

        // Link into City → Region
        public Guid CityId { get; set; }
        public City City { get; set; }

        // BackOffice connection info
        public string BackOfficeDbConnectionString { get; set; }
        public bool UseIsolatedDatabase { get; set; } = true;

        // Portal meta
        public string LogoUrl { get; set; } = string.Empty;
        public string TimeZoneId { get; set; } = string.Empty;
    }

}
