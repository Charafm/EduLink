using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Common.DataObjects.Attendance;

namespace SchoolSaas.Application.Backoffice.Attendance.Queries.GetPaginatedAttendance
{
    public class GetPaginatedAttendanceQuery : IRequest<PagedResult<AttendanceDTO>>
    {
        public AttendanceFilterDTO DTO { get; set; }
    }

    public class GetPaginatedAttendanceQueryHandler : IRequestHandler<GetPaginatedAttendanceQuery, PagedResult<AttendanceDTO>>
    {
        private readonly IAttendanceService _service;

        public GetPaginatedAttendanceQueryHandler(IAttendanceService service)
        {
            _service = service;
        }

        public async Task<PagedResult<AttendanceDTO>> Handle(GetPaginatedAttendanceQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetPaginatedAttendanceRecordsAsync(request.DTO, cancellationToken);
        }
    }
}