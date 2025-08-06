using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Attendance;

namespace SchoolSaas.Application.Backoffice.Attendance.Commands.BulkRecordAttendance
{
    public class BulkRecordAttendanceCommand : IRequest<bool>
    {
        public BulkAttendanceDTO DTO { get; set; }
    }

    public class BulkRecordAttendanceCommandHandler : IRequestHandler<BulkRecordAttendanceCommand, bool>
    {
        private readonly IAttendanceService _service;

        public BulkRecordAttendanceCommandHandler(IAttendanceService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(BulkRecordAttendanceCommand request, CancellationToken cancellationToken)
        {
            return await _service.BulkRecordAttendanceAsync(request.DTO, cancellationToken);
        }
    }
}