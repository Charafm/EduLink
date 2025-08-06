using SchoolSaas.Domain.Common.Enums;

namespace SchoolSaas.Application.Common.Interfaces
{
    public interface INotificationService
    {
        Task NotifyAsync(NotificationTypeEnum type, string message);
        Task NotifyAllAsync(NotificationTypeEnum type, string message);
    }
}
