namespace SchoolSaas.Application.Backoffice.Communications.Commands.UpdateComment;

public record UpdateCommentCommand( 
    UpdateCommentDTO Dto
) : IRequest<bool>;
