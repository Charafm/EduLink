using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Domain.Common.DataObjects.Staff
{
    public class StaffDashboardDTO
    {
        public Guid StaffId { get; set; }
        public string FullName { get; set; }
        public string JobTitle { get; set; }
        public string Department { get; set; }
        public int NotificationsCount { get; set; }
        public List<StaffAuditDTO> RecentActivity { get; set; }
    }
}
