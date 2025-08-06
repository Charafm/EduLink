using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Domain.Common.DataObjects.Edulink
{
    public class CreateSchoolMetadataDTO
    {
        public Guid Id { get; set; }
        public string NameFr { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string Code { get; set; }

        public string AddressFr { get; set; }
        public string AddressAr { get; set; }
        public string AddressEn { get; set; }

        public Guid CityId { get; set; }
        public bool UseIsolatedDatabase { get; set; } = true;
        public string LogoUrl { get; set; } = string.Empty;
        public string TimeZoneId { get; set; } = string.Empty;
    }
}
