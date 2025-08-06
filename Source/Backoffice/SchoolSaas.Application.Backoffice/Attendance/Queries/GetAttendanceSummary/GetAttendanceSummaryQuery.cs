using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Attendance;
using SchoolSaas.Domain.Common.DataObjects.Common;

namespace SchoolSaas.Application.Backoffice.Attendance.Queries.GetAttendanceSummary
    {
    public class GetAttendanceSummaryQuery : IRequest<AttendanceSummaryDTO>
    {
        public Guid Id { get; set; }
        public DateRangeDTO DTO { get; set; }
    }

    public class GetAttendanceSummaryQueryHandler : IRequestHandler<GetAttendanceSummaryQuery, AttendanceSummaryDTO>
    {
        private readonly IAttendanceService _service;

        public GetAttendanceSummaryQueryHandler(IAttendanceService service)
        {
            _service = service;
        }

        public async Task<AttendanceSummaryDTO> Handle(GetAttendanceSummaryQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetAttendanceSummaryAsync(request.Id, request.DTO, cancellationToken);
        }
    }
}