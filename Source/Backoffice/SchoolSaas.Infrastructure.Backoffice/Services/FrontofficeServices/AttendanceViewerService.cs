using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Attendance;
using SchoolSaas.Domain.Common.DataObjects.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Infrastructure.Backoffice.Services.FrontofficeServices
{
    public  class AttendanceViewerService : IAttendanceViewerService
    {
        private readonly IAttendanceService _service;

        public AttendanceViewerService(IAttendanceService service)
        {
            _service = service;
        }
        public async Task<List<AttendanceDTO>> GetAttendanceRecordsAsync(Guid studentId, CancellationToken cancellationToken)
        {
            return await _service.GetAttendanceRecordsAsync(studentId, cancellationToken);
        }
        public async Task<AttendanceSummaryDTO> GetAttendanceSummaryAsync(Guid studentId, DateRangeDTO range, CancellationToken cancellationToken)
        {
            return await _service.GetAttendanceSummaryAsync(studentId, range, cancellationToken);
        }
        public async Task<bool> SubmitAttendanceExcuseAsync(Guid attendanceId, AttendanceExcuseDTO dto, CancellationToken cancellationToken)
        {
            return await _service.SubmitAttendanceExcuseAsync(attendanceId, dto, cancellationToken);
        }
    }
}
