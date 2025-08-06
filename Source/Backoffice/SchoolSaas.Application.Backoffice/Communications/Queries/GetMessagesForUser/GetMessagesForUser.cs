namespace SchoolSaas.Application.Backoffice.Communications.Queries.GetMessagesForUser;

public record GetMessagesForUserQuery(GetMessagesForUserFilterDTO Filter) : IRequest<PagedResult<GetMessagesForUserDTO>>;
