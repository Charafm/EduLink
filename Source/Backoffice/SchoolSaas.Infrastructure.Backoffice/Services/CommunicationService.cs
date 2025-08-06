using Microsoft.EntityFrameworkCore;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Exceptions;
using SchoolSaas.Domain.Common.Entities;
using SchoolSaas.Domain.Common.Enums;
using SchoolSaas.Infrastructure.Backoffice.SignalR;
using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Common.DataObjects.Notification;

namespace SchoolSaas.Infrastructure.Backoffice.Services
{
    public class CommunicationService : ICommunicationService
    {
        private readonly IBackofficeContext _dbContext;
        private readonly IBackofficeReadOnlyContext _dbReadOnlyContext;
        private readonly FrontOfficeHub _frontOfficeHub;
        private readonly BackOfficeHub _backOfficeHub;

        public CommunicationService(
            IBackofficeContext dbContext,
            IBackofficeReadOnlyContext dbReadOnlyContext,
            FrontOfficeHub frontOfficeHub,
            BackOfficeHub backOfficeHub)
        {
            _dbContext = dbContext;
            _dbReadOnlyContext = dbReadOnlyContext;
            _frontOfficeHub = frontOfficeHub;
            _backOfficeHub = backOfficeHub;
        }

        #region Notifications

        public async Task<bool> SendNotificationAsync(AddNotificationRequestDTO data, CancellationToken cancellationToken)
        {
            await _dbContext.BeginTransactionAsync();
            try
            {
                var notificationBodyEntity = new NotificationBody
                {
                    Title = "Test", // In a real scenario, use values from DTO or additional logic
                    Description = "notificationBody"
                };

                await _dbContext.Set<NotificationBody>().AddAsync(notificationBodyEntity, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);

                // Replace with proper user resolution logic
                var userId = Guid.NewGuid();

                await CreateAndSendNotification(userId, notificationBodyEntity, data.Target, cancellationToken);

                await _dbContext.CommitTransactionAsync(cancellationToken);
                return true;
            }
            catch (Exception)
            {
                _dbContext.RollbackTransaction();
                return false;
            }
        }

        public async Task CreateAndSendNotification(Guid userId, NotificationBody notificationBody, NotificationEnum target, CancellationToken cancellationToken)
        {
            var notification = new Notification
            {
                Id = Guid.NewGuid(),
                UserId = userId.ToString(),
                IsRead = false,
                IsSeen = false,
                NotificationBodyId = notificationBody.Id,
                Target = target
            };

            await _dbContext.Set<Notification>().AddAsync(notification, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            // Dispatch via SignalR hubs based on target type
            if (target == NotificationEnum.Portal)
            {
                await _frontOfficeHub.SendNotificationToCitizen(notification.UserId, notificationBody);
            }
            else
            {
                await _backOfficeHub.SendNotificationToAgent(notification.UserId, notificationBody);
            }
        }

        public async Task<Notification> UpdateNotificationStatus(UpdateNotificationStatusRequestDTO data, CancellationToken cancellationToken)
        {
            await _dbContext.BeginTransactionAsync();
            try
            {
                var notification = await _dbContext.Notifications.FindAsync(new object[] { data.NotificationId }, cancellationToken);
                if (notification == null)
                    throw new NotFoundException($"Notification with ID {data.NotificationId} not found.");

                if (data.IsRead.HasValue)
                    notification.IsRead = data.IsRead.Value;
                if (data.IsSeen.HasValue)
                    notification.IsSeen = data.IsSeen.Value;

                _dbContext.Notifications.Update(notification);
                await _dbContext.SaveChangesAsync(cancellationToken);
                await _dbContext.CommitTransactionAsync(cancellationToken);
                return notification;
            }
            catch (Exception)
            {
                _dbContext.RollbackTransaction();
                throw;
            }
        }

        public async Task<GetAllNotificationsByCriteriaResponseDTO> GetAllNotificationsByCriteria(int? page, int? size, GetAllNotificationsByCriteriaRequestDTO criteria)
        {
            var query = _dbContext.Notifications
                .Include(n => n.NotificationBody)
                .IgnoreQueryFilters()
                .AsNoTracking();

            if (!string.IsNullOrEmpty(criteria.UserId))
                query = query.Where(n => n.UserId == criteria.UserId);
            if (criteria.IsRead.HasValue)
                query = query.Where(n => n.IsRead == criteria.IsRead.Value);
            if (criteria.IsSeen.HasValue)
                query = query.Where(n => n.IsSeen == criteria.IsSeen.Value);
            if (criteria.Target.HasValue)
                query = query.Where(n => n.Target == criteria.Target.Value);

            var totalCount = await query.CountAsync();

            if (page.HasValue && size.HasValue)
                query = query.Skip((page.Value - 1) * size.Value).Take(size.Value);

            var notifications = await query.ToListAsync();

            return new GetAllNotificationsByCriteriaResponseDTO
            {
                Notifications = notifications,
                totalCount = totalCount
            };
        }

        #endregion Notifications

        #region Messaging

        public async Task<bool> SendMessageAsync(MessageDTO dto, CancellationToken cancellationToken)
        {
            await _dbContext.BeginTransactionAsync();
            try
            {
                // Create a new Message entity
                var message = new Message
                {
                    Id = Guid.NewGuid(),
                    SenderId = dto.SenderId,
                    TargetId = dto.ReceiverId,
                    // Set Created timestamp from BaseEntity (or assign DateTime.UtcNow)
                };

                // Create corresponding MessageBody entity (assumes one-to-one relationship)
                var messageBody = new MessageBody
                {
   
                    Subject = dto.Subject,
                    Content = dto.Content
                };

                await _dbContext.Messages.AddAsync(message, cancellationToken);
                await _dbContext.MessageBodies.AddAsync(messageBody, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);
                await _dbContext.CommitTransactionAsync(cancellationToken);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                _dbContext.RollbackTransaction();
                return false;
            }
        }

        public async Task<List<MessageDTO>> GetMessagesForUserAsync(string userId, CancellationToken cancellationToken)
        {
            try
            {
                // Assume userId is a GUID stored as string
                var guidUserId = Guid.Parse(userId);
                var messages = await _dbReadOnlyContext.Messages
                    .Where(m => m.TargetId == guidUserId || m.SenderId == guidUserId)
                    .Include(m => m.Body)
                    .AsNoTracking()
                    .OrderByDescending(m => m.Created)
                    .ToListAsync(cancellationToken);

                return messages.Select(m => new MessageDTO
                {

                    SenderId = m.SenderId,
                    ReceiverId = m.TargetId,
                    SentAt = m.Created,
                    Subject = m.Body?.Subject,
                    Content = m.Body?.Content
                }).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new List<MessageDTO>();
            }
        }

        #endregion Messaging

        #region Comments

        public async Task<bool> AddCommentAsync(CommentDTO dto, CancellationToken cancellationToken)
        {
            await _dbContext.BeginTransactionAsync();
            try
            {
                // Create a new Comment entity
                var comment = new Comment
                {
                    Id = Guid.NewGuid(),
                    TargetEntityId = dto.TargetEntityId,
                    Created = DateTime.UtcNow
                };

                // Create the CommentBody entity
                var commentBody = new CommentBody
                {
                    Id = Guid.NewGuid(),
                    // Ideally, CommentBody should have a CommentId FK, so set it here:
                    // We'll assume it's assigned after comment creation
                    SenderId = dto.SenderId,
                    Content = dto.Content
                };

                // For now, simulate a one-to-one relationship by initializing the collection with a single item.
                comment.Body = new List<CommentBody> { commentBody };

                await _dbContext.Comments.AddAsync(comment, cancellationToken);
                await _dbContext.CommentBodies.AddAsync(commentBody, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);
                await _dbContext.CommitTransactionAsync(cancellationToken);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                _dbContext.RollbackTransaction();
                return false;
            }
        }

        public async Task<bool> UpdateCommentAsync(Guid commentId, CommentDTO dto, CancellationToken cancellationToken)
        {
            await _dbContext.BeginTransactionAsync();
            try
            {
                var comment = await _dbContext.Comments
                    .Include(c => c.Body)
                    .FirstOrDefaultAsync(c => c.Id == commentId, cancellationToken);
                if (comment == null)
                    throw new KeyNotFoundException("Comment not found.");

                // Update the comment's target if needed
                comment.TargetEntityId = dto.TargetEntityId;
                // Optionally update timestamp, etc.
                comment.LastModified = DateTime.UtcNow;

                // Assume one-to-one relationship: update the first (and only) CommentBody
                var commentBody = comment.Body.FirstOrDefault();
                if (commentBody == null)
                    throw new KeyNotFoundException("Comment body not found.");

                commentBody.Content = dto.Content;

                _dbContext.Comments.Update(comment);
                _dbContext.CommentBodies.Update(commentBody);
                await _dbContext.SaveChangesAsync(cancellationToken);
                await _dbContext.CommitTransactionAsync(cancellationToken);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                _dbContext.RollbackTransaction();
                return false;
            }
        }
        public async Task<bool> MarkAllNotificationsAsReadAsync(string userId, CancellationToken ct)
        {
            var notifications = await _dbContext.Notifications
                .Where(n => n.UserId == userId && !n.IsRead)
                .ToListAsync(ct);

            foreach (var n in notifications)
            {
                n.IsRead = true;
                n.IsSeen = true;
            }

            await _dbContext.SaveChangesAsync(ct);
            return true;
        }
        public async Task<List<CommentDTO>> GetCommentsForEntityAsync(Guid targetEntityId, CancellationToken cancellationToken)
        {
            try
            {
                var comments = await _dbReadOnlyContext.Comments
                    .Where(c => c.TargetEntityId == targetEntityId)
                    .Include(c => c.Body)
                    .AsNoTracking()
                    .OrderBy(c => c.Created)
                    .ToListAsync(cancellationToken);

                return comments.Select(c =>
                {
                    // For simplicity, assume a one-to-one relationship and take the first CommentBody.
                    var body = c.Body.FirstOrDefault();
                    return new CommentDTO
                    {

                        SenderId = body?.SenderId ?? Guid.Empty,
                        TargetEntityId = c.TargetEntityId ?? Guid.Empty,
                        CreatedAt = c.Created,
                        Content = body?.Content
                    };
                }).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new List<CommentDTO>();
            }
        }

        #endregion Comments
    }
}
