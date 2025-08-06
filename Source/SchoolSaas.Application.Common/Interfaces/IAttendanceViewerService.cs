using SchoolSaas.Domain.Common.DataObjects.Attendance;
using SchoolSaas.Domain.Common.DataObjects.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Application.Common.Interfaces
{
    public interface IAttendanceViewerService
    {
        Task<List<AttendanceDTO>> GetAttendanceRecordsAsync(Guid studentId, CancellationToken cancellationToken);
        Task<AttendanceSummaryDTO> GetAttendanceSummaryAsync(Guid studentId, DateRangeDTO range, CancellationToken cancellationToken);
        Task<bool> SubmitAttendanceExcuseAsync(Guid attendanceId, AttendanceExcuseDTO dto, CancellationToken cancellationToken);

    }
}
