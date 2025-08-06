using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Attendance;
using SchoolSaas.Domain.Common.DataObjects.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Application.Frontoffice.Attendance
{
    public class GetAttendanceSummaryQuery : IRequest<AttendanceSummaryDTO>
    {
        public Guid StudentId { get; set; }
        public DateRangeDTO DateRange { get; set; }
    }

    public class GetAttendanceSummaryQueryHandler : IRequestHandler<GetAttendanceSummaryQuery, AttendanceSummaryDTO>
    {
        private readonly IBackofficeConnectedService _service;

        public GetAttendanceSummaryQueryHandler(IBackofficeConnectedService service) => _service = service;

        public async Task<AttendanceSummaryDTO> Handle(GetAttendanceSummaryQuery request, CancellationToken cancellationToken)
        {
            return (await _service.GetAttendanceSummary(request.StudentId, request.DateRange, cancellationToken)).Data;
        }
    }
}
