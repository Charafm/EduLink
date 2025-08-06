using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Attendance;

namespace SchoolSaas.Application.Backoffice.Attendance.Queries.GetAttendanceRecords
{
    public class GetAttendanceRecordsQuery : IRequest<List<AttendanceDTO>>
    {
        public Guid Id { get; set; }
    }

    public class GetAttendanceRecordsQueryHandler : IRequestHandler<GetAttendanceRecordsQuery, List<AttendanceDTO>>
    {
        private readonly IAttendanceService _service;

        public GetAttendanceRecordsQueryHandler(IAttendanceService service)
        {
            _service = service;
        }

        public async Task<List<AttendanceDTO>> Handle(GetAttendanceRecordsQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetAttendanceRecordsAsync(request.Id, cancellationToken);
        }
    }
}