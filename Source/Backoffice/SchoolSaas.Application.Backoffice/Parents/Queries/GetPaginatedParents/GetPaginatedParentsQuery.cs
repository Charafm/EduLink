using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Common.DataObjects.Parent;

namespace SchoolSaas.Application.Backoffice.Parents.Queries.GetPaginatedParents
{
    public class GetPaginatedParentsQuery : IRequest<PagedResult<ParentDTO>>
    {
        public ParentFilterDTO DTO { get; set; }
    }

    public class GetPaginatedParentsQueryHandler : IRequestHandler<GetPaginatedParentsQuery, PagedResult<ParentDTO>>
    {
        private readonly IParentService _service;

        public GetPaginatedParentsQueryHandler(IParentService service)
        {
            _service = service;
        }

        public async Task<PagedResult<ParentDTO>> Handle(GetPaginatedParentsQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetPaginatedParentsAsync(request.DTO,cancellationToken);
        }
    }
}