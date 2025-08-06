using SchoolSaas.Domain.Common.DataObjects.Common;
using SchoolSaas.Domain.Common.DataObjects.Notification;
using SchoolSaas.Domain.Common.DataObjects.Student;

namespace SchoolSaas.Domain.Common.DataObjects.Parent
{
    public class ParentDashboardDTO
    {
        public Guid ParentId { get; set; }
        public List<NotificationDTO> Notifications { get; set; }
        public List<AssociatedStudentStatusDTO> Students { get; set; }
        public List<RecentActivityDTO> RecentActivities { get; set; }
    }
}
