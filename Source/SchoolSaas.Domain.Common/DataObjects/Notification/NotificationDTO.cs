namespace SchoolSaas.Domain.Common.DataObjects.Notification
{
    public class NotificationDTO
    {
        public Guid NotificationId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }
    }
    
}
