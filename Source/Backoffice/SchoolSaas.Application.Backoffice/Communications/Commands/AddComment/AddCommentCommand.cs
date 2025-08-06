namespace SchoolSaas.Application.Backoffice.Communications.Commands.AddComment;

public record AddCommentCommand( 
    AddCommentDTO Dto
) : IRequest<bool>;
