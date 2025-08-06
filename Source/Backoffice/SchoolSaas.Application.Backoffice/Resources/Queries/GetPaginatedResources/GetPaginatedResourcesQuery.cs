using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Common.DataObjects.Grade;
using SchoolSaas.Domain.Common.DataObjects.SchoolSupply;

namespace SchoolSaas.Application.Backoffice.Resources.Queries.GetPaginatedResources
{
    public class GetPaginatedResourcesQuery : IRequest<PagedResult<GradeResourceDTO>>
    {
        public ResourceFilterDTO DTO { get; set; }
    }

    public class GetPaginatedResourcesQueryHandler : IRequestHandler<GetPaginatedResourcesQuery, PagedResult<GradeResourceDTO>>
    {
        private readonly ISchoolSupplyService _service;

        public GetPaginatedResourcesQueryHandler(ISchoolSupplyService service)
        {
            _service = service;
        }

        public async Task<PagedResult<GradeResourceDTO>> Handle(GetPaginatedResourcesQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetPaginatedResourcesAsync(request.DTO, cancellationToken);
        }
    }
}