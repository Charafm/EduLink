using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Common.DataObjects.Attendance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Application.Backoffice.Frontoffice.AttendanceViewer
{
    
    public class GetAttendanceRecordsQuery : IRequest<List<AttendanceDTO>>
    {
        public Guid Id { get; set; }
    }

    public class GetAttendanceRecordsQueryHandler : IRequestHandler<GetAttendanceRecordsQuery, List<AttendanceDTO>>
    {
        private readonly IAttendanceViewerService _service;

        public GetAttendanceRecordsQueryHandler(IAttendanceViewerService service)
        {
            _service = service;
        }

        public async Task<List<AttendanceDTO>> Handle(GetAttendanceRecordsQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetAttendanceRecordsAsync(request.Id, cancellationToken);
        }
    }
}
