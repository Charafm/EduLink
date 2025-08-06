using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Domain.Common.DataObjects.Edulink
{
    public class BranchDto
    {
        public Guid Id { get; set; }
        public Guid SchoolId { get; set; }
        public string BranchNameFr { get; set; } = string.Empty;
        public string BranchNameAr { get; set; } = string.Empty;
        public string? BranchNameEn { get; set; } = string.Empty;
        public string AddressFr { get; set; } = string.Empty;
        public string AddressAr { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string PrincipalNameFr { get; set; } = string.Empty;
        public string PrincipalNameAr { get; set; } = string.Empty;
        public Guid CityId { get; set; }

    }
}
