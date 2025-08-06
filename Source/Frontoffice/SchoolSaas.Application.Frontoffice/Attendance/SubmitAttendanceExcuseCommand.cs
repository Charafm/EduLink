using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Attendance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Application.Frontoffice.Attendance
{
    public class SubmitAttendanceExcuseCommand : IRequest<bool>
    {
        public Guid StudentId { get; set; }
        public AttendanceExcuseDTO Excuse { get; set; }
    }

    public class SubmitAttendanceExcuseCommandHandler : IRequestHandler<SubmitAttendanceExcuseCommand, bool>
    {
        private readonly IBackofficeConnectedService _service;

        public SubmitAttendanceExcuseCommandHandler(IBackofficeConnectedService service) => _service = service;

        public async Task<bool> Handle(SubmitAttendanceExcuseCommand request, CancellationToken cancellationToken)
        {
            return (await _service.SubmitAttendanceExcuse(request.StudentId, request.Excuse, cancellationToken)).Data;
        }
    }
}
