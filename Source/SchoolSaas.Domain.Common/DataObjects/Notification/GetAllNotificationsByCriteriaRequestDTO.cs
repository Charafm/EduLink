using SchoolSaas.Domain.Common.Entities;
using SchoolSaas.Domain.Common.Enums;

namespace SchoolSaas.Domain.Common.DataObjects.Notification
{
    public class GetAllNotificationsByCriteriaRequestDTO
    {
        public NotificationEnum? Target { get; set; }
        public string UserId{ get; set; }
        public bool? IsRead { get; set; }
        public bool? IsSeen { get; set; }
    }
    public class GetAllNotificationsByCriteriaResponseDTO
    {
        public List<SchoolSaas.Domain.Common.Entities.Notification> Notifications { get; set; }
        public int totalCount { get; set; }

    }

}
