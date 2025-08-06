using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Attendance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Application.Backoffice.Frontoffice.AttendanceViewer
{
   
    public class SubmitAttendanceExcuseCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public AttendanceExcuseDTO Data { get; set; }
    }

    public class SubmitAttendanceExcuseCommandHandler : IRequestHandler<SubmitAttendanceExcuseCommand, bool>
    {
        private readonly IAttendanceViewerService _service;

        public SubmitAttendanceExcuseCommandHandler(IAttendanceViewerService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(SubmitAttendanceExcuseCommand request, CancellationToken cancellationToken)
        {
            return await _service.SubmitAttendanceExcuseAsync(request.Id, request.Data, cancellationToken);
        }
    }
}
