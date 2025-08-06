using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Common.DataObjects.Attendance;
using SchoolSaas.Domain.Common.DataObjects.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Application.Backoffice.Frontoffice.AttendanceViewer
{
    
    public class GetAttendanceSummaryQuery : IRequest<AttendanceSummaryDTO>
    {
        public Guid Id { get; set; }
        public DateRangeDTO Data {  get; set; }
    }

    public class GetAttendanceSummaryQueryHandler : IRequestHandler<GetAttendanceSummaryQuery, AttendanceSummaryDTO>
    {
        private readonly IAttendanceViewerService _service;

        public GetAttendanceSummaryQueryHandler(IAttendanceViewerService service)
        {
            _service = service;
        }

        public async Task<AttendanceSummaryDTO> Handle(GetAttendanceSummaryQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetAttendanceSummaryAsync(request.Id,request.Data, cancellationToken);
        }
    }
}
