namespace SchoolSaas.Application.Backoffice.Communications.Commands.SendMessage;

public record SendMessageCommand(SendMessageDTO Dto) : IRequest<bool>;
