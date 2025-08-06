namespace SchoolSaas.Application.Backoffice.Communications.Queries.GetCommentsForEntity;

public record GetCommentsForEntityQuery( 
    GetCommentsForEntityFilterDTO Filter
) : IRequest<PagedResult<GetCommentsForEntityDTO>>;
