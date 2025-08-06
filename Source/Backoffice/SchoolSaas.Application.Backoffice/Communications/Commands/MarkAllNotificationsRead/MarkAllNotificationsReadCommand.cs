namespace SchoolSaas.Application.Backoffice.Communications.Commands.MarkAllNotificationsRead;

public record MarkAllNotificationsReadCommand( 
    MarkAllNotificationsReadDTO Dto
) : IRequest<bool>;
