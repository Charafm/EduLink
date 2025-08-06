namespace SchoolSaas.Application.Backoffice.Communications.Queries.GetAllNotifications;

public record GetAllNotificationsQuery(GetAllNotificationsFilterDTO Filter) : IRequest<PagedResult<GetAllNotificationsDTO>>;
