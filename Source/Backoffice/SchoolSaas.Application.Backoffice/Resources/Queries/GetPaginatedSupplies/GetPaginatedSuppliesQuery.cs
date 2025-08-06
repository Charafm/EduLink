using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Common.DataObjects.SchoolSupply;

namespace SchoolSaas.Application.Backoffice.Resources.Queries.GetPaginatedSupplies
{
    public class GetPaginatedSuppliesQuery : IRequest<PagedResult<SchoolSupplyDTO>>
    {
        public SchoolSupplyFilterDTO DTO { get; set; }
    }

    public class GetPaginatedSuppliesQueryHandler : IRequestHandler<GetPaginatedSuppliesQuery, PagedResult<SchoolSupplyDTO>>
    {
        private readonly ISchoolSupplyService _service;

        public GetPaginatedSuppliesQueryHandler(ISchoolSupplyService service)
        {
            _service = service;
        }

        public async Task<PagedResult<SchoolSupplyDTO>> Handle(GetPaginatedSuppliesQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetPaginatedSuppliesAsync(request.DTO, cancellationToken);
        }
    }
}