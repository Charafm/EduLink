using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Common.DataObjects.Common;
using SchoolSaas.Domain.Common.DataObjects.Schedule;
using SchoolSaas.Domain.Common.DataObjects.Teacher;

namespace SchoolSaas.Application.Common.Interfaces
{
    public interface IScheduleService
    {
        Task<bool> CreateScheduleAsync(ScheduleDTO dto, CancellationToken cancellationToken);
        Task<bool> UpdateScheduleAsync(Guid scheduleId, ScheduleDTO dto, CancellationToken cancellationToken);
        Task<bool> DeleteScheduleAsync(Guid scheduleId, CancellationToken cancellationToken);
        Task<List<ScheduleDTO>> GetScheduleByGradeSectionAsync(Guid gradeSectionId, CancellationToken cancellationToken);
        Task<List<ScheduleDTO>> GetTeacherScheduleAsync(Guid teacherId, CancellationToken cancellationToken);
        Task<List<ScheduleDTO>> GetClassroomScheduleAsync(Guid classroomId, CancellationToken cancellationToken);
        Task<bool> CheckScheduleConflictsAsync(ScheduleConflictCheckDTO dto, CancellationToken cancellationToken);
        Task<List<ScheduleDTO>> AutoGenerateSchedulesAsync(ScheduleConstraintsDTO constraints, CancellationToken cancellationToken);
        Task<ScheduleDashboardMetricsDTO> GetScheduleDashboardMetricsAsync(DateRangeDTO dateRange, CancellationToken cancellationToken);

        Task<bool> RescheduleAsync(Guid scheduleId, TimeOnly newStartTime, TimeOnly newEndTime, CancellationToken cancellationToken);

        Task<PagedResult<TeacherAssignmentDTO>> GetTeacherAssignmentsAsync(TeacherAssignmentFilterDTO filter, CancellationToken cancellationToken);
    }
}
