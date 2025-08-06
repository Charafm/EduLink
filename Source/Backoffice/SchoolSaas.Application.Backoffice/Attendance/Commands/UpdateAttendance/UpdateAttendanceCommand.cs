using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Attendance;
using SchoolSaas.Domain.Common.Enums;

namespace SchoolSaas.Application.Backoffice.Attendance.Commands.UpdateAttendance
{
    public class UpdateAttendanceCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public AttendanceDTO DTO { get; set; }
        public AttendanceChangeReasonEnum ReasonEnum { get; set; }
    }

    public class UpdateAttendanceCommandHandler : IRequestHandler<UpdateAttendanceCommand, bool>
    {
        private readonly IAttendanceService _service;

        public UpdateAttendanceCommandHandler(IAttendanceService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(UpdateAttendanceCommand request, CancellationToken cancellationToken)
        {
            return await _service.UpdateAttendanceAsync(request.Id, request.DTO, request.ReasonEnum, cancellationToken); 
        }
    }
}