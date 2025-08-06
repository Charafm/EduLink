using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Attendance;

namespace SchoolSaas.Application.Backoffice.Attendance.Queries.GetAttendanceById
{
    public class GetAttendanceByIdQuery : IRequest<AttendanceDTO>
    {
        public Guid Id { get; set; }
    }

    public class GetAttendanceByIdQueryHandler : IRequestHandler<GetAttendanceByIdQuery, AttendanceDTO>
    {
        private readonly IAttendanceService _service;

        public GetAttendanceByIdQueryHandler(IAttendanceService service)
        {
            _service = service;
        }

        public async Task<AttendanceDTO> Handle(GetAttendanceByIdQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetAttendanceByIdAsync(request.Id, cancellationToken);
        }
    }
}