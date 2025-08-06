using SchoolSaas.Domain.Common.DataObjects.Common;
using SchoolSaas.Domain.Common.Enums;


namespace SchoolSaas.Domain.Common.Entities
{
    public class Notification : BaseEntity<Guid>
    {
        public NotificationEnum Target { get; set; }
        public bool IsRead { get; set; }
        public bool IsSeen{ get; set; }
        public Guid NotificationBodyId { get; set; }
        public NotificationBody? NotificationBody { get; set; }
        public string? UserId { get; set; }
        public UserDto? User { get; set; }
    }
}
