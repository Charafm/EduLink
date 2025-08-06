using SchoolSaas.Domain.Common.Entities;

namespace SchoolSaas.Application.Common.Models
{
    public class GetNotificationResponseDto
    {
        public List<Notification> Notifications { get; set; }
        public int totalCount { get; set; }
    }
}
