namespace SchoolSaas.Application.Backoffice.Communications.Commands.CreateAndSendNotification;

public record CreateAndSendNotificationCommand(CreateAndSendNotificationDTO Dto) : IRequest<bool>;
