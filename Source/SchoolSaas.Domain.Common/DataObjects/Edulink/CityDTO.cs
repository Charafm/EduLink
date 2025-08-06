using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Domain.Common.DataObjects.Edulink
{
    public class CityDTO
    {
        public Guid Id { get; set; }
        public string NameFr { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public Guid RegionId { get; set; }
    }
}
