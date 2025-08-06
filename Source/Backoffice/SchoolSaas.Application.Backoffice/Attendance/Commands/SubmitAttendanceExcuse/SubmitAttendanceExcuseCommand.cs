using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Attendance;

namespace SchoolSaas.Application.Backoffice.Attendance.Commands.SubmitAttendanceExcuse
{
    public class SubmitAttendanceExcuseCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public AttendanceExcuseDTO DTO { get; set; }
    }

    public class SubmitAttendanceExcuseCommandHandler : IRequestHandler<SubmitAttendanceExcuseCommand, bool>
    {
        private readonly IAttendanceService _service;

        public SubmitAttendanceExcuseCommandHandler(IAttendanceService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(SubmitAttendanceExcuseCommand request, CancellationToken cancellationToken)
        {
            return await _service.SubmitAttendanceExcuseAsync(request.Id, request.DTO, cancellationToken); 
        }
    }
}