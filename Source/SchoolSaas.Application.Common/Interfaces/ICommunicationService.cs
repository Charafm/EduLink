using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Common.DataObjects.Notification;
using SchoolSaas.Domain.Common.Entities;
using SchoolSaas.Domain.Common.Enums;

namespace SchoolSaas.Application.Common.Interfaces
{
    public interface ICommunicationService
    {
        // Notification methods
        Task<bool> SendNotificationAsync(AddNotificationRequestDTO data, CancellationToken cancellationToken);
        Task<Notification> UpdateNotificationStatus(UpdateNotificationStatusRequestDTO data, CancellationToken cancellationToken);
        Task<GetAllNotificationsByCriteriaResponseDTO> GetAllNotificationsByCriteria(int? page, int? size, GetAllNotificationsByCriteriaRequestDTO criteria);
        Task CreateAndSendNotification(Guid userId, NotificationBody notificationBody, NotificationEnum target, CancellationToken cancellationToken);
        Task<bool> MarkAllNotificationsAsReadAsync(string userId, CancellationToken ct);
        // Messaging methods
        Task<bool> SendMessageAsync(MessageDTO dto, CancellationToken cancellationToken);
        Task<List<MessageDTO>> GetMessagesForUserAsync(string userId, CancellationToken cancellationToken);

        // Comment methods
        Task<bool> AddCommentAsync(CommentDTO dto, CancellationToken cancellationToken);
        Task<bool> UpdateCommentAsync(Guid commentId, CommentDTO dto, CancellationToken cancellationToken);
        Task<List<CommentDTO>> GetCommentsForEntityAsync(Guid targetEntityId, CancellationToken cancellationToken);

    }
}
