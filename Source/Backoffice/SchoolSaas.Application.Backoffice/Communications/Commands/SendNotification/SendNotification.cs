namespace SchoolSaas.Application.Backoffice.Communications.Commands.SendNotification;

public record SendNotificationCommand(SendNotificationDTO Dto) : IRequest<bool>;
