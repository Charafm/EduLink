using SchoolSaas.Domain.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Domain.Edulink
{
    public class City : BaseEntity<Guid>
    {
        public string NameFr { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }

        public Guid RegionId { get; set; }
        public Region Region { get; set; }
    }
}
