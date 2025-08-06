using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Domain.Common.DataObjects.Edulink
{
    public class SchoolMetadataDTO
    {
        public Guid Id { get; set; }

        // Localized names
        public string NameFr { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }

        public string Code { get; set; }

        // Localized addresses
        public string AddressFr { get; set; }
        public string AddressAr { get; set; }
        public string AddressEn { get; set; }

        // Region via City → Region
        public Guid RegionId { get; set; }
        public string RegionNameFr { get; set; }
        public string RegionNameAr { get; set; }
        public string RegionNameEn { get; set; }

        // City
        public Guid CityId { get; set; }
        public string CityNameFr { get; set; }
        public string CityNameAr { get; set; }
        public string CityNameEn { get; set; }

        // BackOffice DB
        public bool UseIsolatedDatabase { get; set; }
        public bool HasCustomConnectionString { get; set; }

        // Portal
        public string LogoUrl { get; set; } = string.Empty;
        public string TimeZoneId { get; set; } = string.Empty;
    }

}
