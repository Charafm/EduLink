using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Common.DataObjects.Staff;

namespace SchoolSaas.Application.Backoffice.Staff.Queries.GetPaginatedStaff
{
    public class GetPaginatedStaffQuery : IRequest<PagedResult<StaffDTO>>
    {
        public StaffFilterDTO DTO   { get; set; }

    }

    public class GetPaginatedStaffQueryHandler : IRequestHandler<GetPaginatedStaffQuery, PagedResult<StaffDTO>>
    {
        private readonly IStaffService _service;

        public GetPaginatedStaffQueryHandler(IStaffService service)
        {
            _service = service;
        }

        public async Task<PagedResult<StaffDTO>> Handle(GetPaginatedStaffQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetPaginatedStaffAsync(request.DTO, cancellationToken);
        }
    }
}