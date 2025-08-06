using MediatR;
using SchoolSaas.Application.Common.Interfaces;

namespace SchoolSaas.Application.Backoffice.Attendance.Commands.DeleteAttendance
{
    public class DeleteAttendanceCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }

    public class DeleteAttendanceCommandHandler : IRequestHandler<DeleteAttendanceCommand, bool>
    {
        private readonly IAttendanceService _service;

        public DeleteAttendanceCommandHandler(IAttendanceService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(DeleteAttendanceCommand request, CancellationToken cancellationToken)
        {
            return await _service.DeleteAttendanceAsync(request.Id, cancellationToken);
        }
    }
}