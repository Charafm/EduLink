using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Attendance;

namespace SchoolSaas.Application.Backoffice.Attendance.Commands.RecordAttendance
{
    public class RecordAttendanceCommand : IRequest<bool>
    {
        public AttendanceDTO DTO { get; set; }
    }

    public class RecordAttendanceCommandHandler : IRequestHandler<RecordAttendanceCommand, bool>
    {
        private readonly IAttendanceService _service;

        public RecordAttendanceCommandHandler(IAttendanceService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(RecordAttendanceCommand request, CancellationToken cancellationToken)
        {
            return await _service.RecordAttendanceAsync(request.DTO, cancellationToken);
        }
    }

}

