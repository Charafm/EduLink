namespace SchoolSaas.Application.Backoffice.Communications.Commands.UpdateNotificationStatus;

public record UpdateNotificationStatusCommand( 
    UpdateNotificationStatusDTO Dto
) : IRequest<bool>;
