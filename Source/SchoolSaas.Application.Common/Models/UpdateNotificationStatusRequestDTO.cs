namespace SchoolSaas.Application.Common.Models
{
    public class UpdateNotificationStatusRequestDTO
    {
        public Guid NotificationId { get; set; }
        public bool? IsRead { get; set; }
        public bool? IsSeen { get; set; }
    }
}
