using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Common.DataObjects.Attendance;
using SchoolSaas.Domain.Common.DataObjects.Common;
using SchoolSaas.Domain.Common.Enums;

namespace SchoolSaas.Application.Common.Interfaces
{
    public interface IAttendanceService
    {
        Task<bool> RecordAttendanceAsync(AttendanceDTO data, CancellationToken cancellationToken);
        Task<bool> UpdateAttendanceAsync(Guid attendanceId, AttendanceDTO data, AttendanceChangeReasonEnum reason, CancellationToken cancellationToken);
        Task<List<AttendanceDTO>> GetAttendanceRecordsAsync(Guid studentId, CancellationToken cancellationToken);
        Task<AttendanceDTO> GetAttendanceByIdAsync(Guid attendanceId, CancellationToken cancellationToken); // ✅ NEW
        Task<PagedResult<AttendanceDTO>> GetPaginatedAttendanceRecordsAsync(AttendanceFilterDTO filter, CancellationToken cancellationToken); // ✅ NEW
        Task<PagedResult<AttendanceHistoryDTO>> GetPaginatedAttendanceHistoryAsync(AttendanceHistoryFilterDTO filter, CancellationToken cancellationToken);
        Task<bool> BulkRecordAttendanceAsync(BulkAttendanceDTO dto, CancellationToken cancellationToken);
        Task<AttendanceSummaryDTO> GetAttendanceSummaryAsync(Guid studentId, DateRangeDTO dateRange, CancellationToken cancellationToken);
        Task<bool> SubmitAttendanceExcuseAsync(Guid attendanceId, AttendanceExcuseDTO dto, CancellationToken cancellationToken);
        Task<bool> DeleteAttendanceAsync(Guid attendanceId, CancellationToken cancellationToken); // ✅ NEW

    }
}
