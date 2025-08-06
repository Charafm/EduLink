using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Common.DataObjects.Attendance;

namespace SchoolSaas.Application.Backoffice.Attendance.Queries.GetAttendanceHistory
{
    public class GetAttendanceHistoryQuery : IRequest<PagedResult<AttendanceHistoryDTO>>
    {
        public AttendanceHistoryFilterDTO DTO { get; set; }
    }

    public class GetAttendanceHistoryQueryHandler : IRequestHandler<GetAttendanceHistoryQuery, PagedResult<AttendanceHistoryDTO>>
    {
        private readonly IAttendanceService _service;

        public GetAttendanceHistoryQueryHandler(IAttendanceService service)
        {
            _service = service;
        }

        public async Task<PagedResult<AttendanceHistoryDTO>> Handle(GetAttendanceHistoryQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetPaginatedAttendanceHistoryAsync(request.DTO, cancellationToken);
        }
    }
}