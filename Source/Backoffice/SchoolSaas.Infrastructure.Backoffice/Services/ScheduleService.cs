using Microsoft.EntityFrameworkCore;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Backoffice.Academic;
using SchoolSaas.Domain.Backoffice.Resources;
using SchoolSaas.Domain.Common.DataObjects.Common;
using SchoolSaas.Domain.Common.DataObjects.Course;
using SchoolSaas.Domain.Common.DataObjects.Schedule;
using SchoolSaas.Domain.Common.DataObjects.Teacher;

namespace SchoolSaas.Infrastructure.Backoffice.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly IBackofficeContext _dbContext;
        private readonly IBackofficeReadOnlyContext _dbReadOnlyContext;
        private readonly IServiceHelper _serviceHelper;

        public ScheduleService(
            IBackofficeContext dbContext,
            IBackofficeReadOnlyContext dbReadOnlyContext,
            IServiceHelper serviceHelper)
        {
            _dbContext = dbContext;
            _dbReadOnlyContext = dbReadOnlyContext;
            _serviceHelper = serviceHelper;
        }

        public async Task<bool> CreateScheduleAsync(ScheduleDTO dto, CancellationToken cancellationToken)
        {
            return await _serviceHelper.ExecuteWithTransactionAsync(async () =>
            {
                var schedule = MapToEntity(dto);
                await _dbContext.Schedules.AddAsync(schedule, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return true;
            });
        }

        public async Task<bool> UpdateScheduleAsync(Guid scheduleId, ScheduleDTO dto, CancellationToken cancellationToken)
        {
            return await _serviceHelper.ExecuteWithTransactionAsync(async () =>
            {
                var schedule = await _dbContext.Schedules.FirstOrDefaultAsync(s => s.Id == scheduleId, cancellationToken)
                    ?? throw new KeyNotFoundException("Schedule not found.");

                schedule.CourseId = dto.CourseId;
                schedule.ClassroomId = dto.ClassroomId;
                schedule.DayOfWeek = dto.DayOfWeek;
                schedule.StartTime = dto.StartTime;
                schedule.EndTime = dto.EndTime;
                schedule.GradeSectionId = dto.GradeSectionId;

                _dbContext.Schedules.Update(schedule);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return true;
            });
        }

        public async Task<bool> DeleteScheduleAsync(Guid scheduleId, CancellationToken cancellationToken)
        {
            return await _serviceHelper.ExecuteWithTransactionAsync(async () =>
            {
                var schedule = await _dbContext.Schedules.FirstOrDefaultAsync(s => s.Id == scheduleId, cancellationToken)
                    ?? throw new KeyNotFoundException("Schedule not found.");

                _dbContext.Schedules.Remove(schedule);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return true;
            });
        }

        public async Task<List<ScheduleDTO>> GetScheduleByGradeSectionAsync(Guid gradeSectionId, CancellationToken cancellationToken)
        {
            var schedules = await _dbReadOnlyContext.Schedules
                .AsNoTracking()
                .Where(s => s.GradeSectionId == gradeSectionId)
                .ToListAsync(cancellationToken);

            return schedules.Select(MapToDto).ToList();
        }

        public async Task<List<ScheduleDTO>> GetTeacherScheduleAsync(Guid teacherId, CancellationToken cancellationToken)
        {
            return  _dbReadOnlyContext.Schedules
                .AsNoTracking()
                .Where(s => s.TeacherId == teacherId)
                .OrderBy(s => s.DayOfWeek).ThenBy(s => s.StartTime)
                .Select(MapToDto)
                .ToList();
        }

        public async Task<List<ScheduleDTO>> GetClassroomScheduleAsync(Guid classroomId, CancellationToken cancellationToken)
        {
            return  _dbReadOnlyContext.Schedules
                .AsNoTracking()
                .Where(s => s.ClassroomId == classroomId)
                .OrderBy(s => s.DayOfWeek).ThenBy(s => s.StartTime)
                .Select(MapToDto).ToList();
        }
        public async Task<bool> RescheduleAsync(Guid scheduleId, TimeOnly newStartTime, TimeOnly newEndTime, CancellationToken cancellationToken)
        {
            return await _serviceHelper.ExecuteWithTransactionAsync(async () =>
            {
                var schedule = await _dbContext.Schedules
                    .FirstOrDefaultAsync(s => s.Id == scheduleId, cancellationToken)
                    ?? throw new KeyNotFoundException("Schedule not found.");

                schedule.StartTime = newStartTime;
                schedule.EndTime = newEndTime;
                schedule.LastModified = DateTime.UtcNow;

                _dbContext.Schedules.Update(schedule);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return true;
            });
        }


        public async Task<ScheduleDashboardMetricsDTO> GetScheduleDashboardMetricsAsync(DateRangeDTO dateRange, CancellationToken cancellationToken)
        {
            var schedules = await _dbReadOnlyContext.Schedules
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            int totalClasses = schedules.Count;

            // Conflicts: same classroom & day, overlapping times
            int conflicts = schedules
                .GroupBy(s => new { s.ClassroomId, s.DayOfWeek })
                .Sum(g => g.Count() - g
                    .GroupBy(x => new { x.StartTime, x.EndTime })
                    .Count()
                );

            double daysInRange = (dateRange.EndDate.Date - dateRange.StartDate.Date).TotalDays + 1;
            double avgDaily = daysInRange > 0 ? totalClasses / daysInRange : 0;

            return new ScheduleDashboardMetricsDTO
            {
                TotalClassesScheduled = totalClasses,
                TotalConflicts = conflicts,
                AverageDailyClasses = avgDaily
            };
        }

        public async Task<List<ScheduleDTO>> AutoGenerateSchedulesAsync(ScheduleConstraintsDTO constraints, CancellationToken cancellationToken)
        {
            // Implementation would depend on your scheduling algorithm
            // This is a simplified version
            var generatedSchedules = new List<ScheduleDTO>();

            foreach (var courseSection in constraints.CourseSections)
            {
                var availableClassrooms = await FindAvailableClassrooms(courseSection, constraints);
                var schedule = new ScheduleDTO
                {
                    CourseId = courseSection.CourseId,
                    GradeSectionId = courseSection.GradeSectionId,
                    ClassroomId = availableClassrooms.First().Id,
                    DayOfWeek = courseSection.PreferredDays.First(),
                    StartTime = courseSection.PreferredStartTime,
                    EndTime = courseSection.PreferredStartTime.Add(TimeSpan.FromHours(1))
                };
                generatedSchedules.Add(schedule);
            }

            return generatedSchedules;
        }

        public async Task<bool> CheckScheduleConflictsAsync(ScheduleConflictCheckDTO dto, CancellationToken cancellationToken)
        {
            return await _dbReadOnlyContext.Schedules
                .AnyAsync(s => s.TeacherId == dto.TeacherId &&
                              s.DayOfWeek == dto.DayOfWeek &&
                              s.StartTime < dto.EndTime &&
                              s.EndTime > dto.StartTime,
                    cancellationToken);
        }

        public async Task<PagedResult<TeacherAssignmentDTO>> GetTeacherAssignmentsAsync(
            TeacherAssignmentFilterDTO filter, CancellationToken cancellationToken)
        {
            var query = _dbReadOnlyContext.TeacherCourseAssignments
                .Include(a => a.Teacher)
                .Include(a => a.Course)
                .Include(a => a.AcademicYear)
                .AsQueryable();

            if (filter.TeacherId.HasValue)
                query = query.Where(a => a.TeacherId == filter.TeacherId.Value);

            if (filter.AcademicYearId.HasValue)
                query = query.Where(a => a.AcademicYearId == filter.AcademicYearId.Value);

            var totalCount = await query.CountAsync(cancellationToken);
            var results = await query
                .OrderBy(a => a.AcademicYear.StartYear)
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .Select(a => new TeacherAssignmentDTO
                {
                    TeacherId = a.TeacherId,
                    CourseId = a.CourseId,
                    AcademicYearId = a.AcademicYearId,
                    TeacherName = $"{a.Teacher.FirstNameFr} {a.Teacher.LastNameFr}",
                    CourseTitle = a.Course.TitleFr,
                    AcademicYear = $"{a.AcademicYear.StartYear}-{a.AcademicYear.EndYear}"
                })
                .ToListAsync(cancellationToken);

            return new PagedResult<TeacherAssignmentDTO>
            {
                Results = results,
                CurrentPage = filter.PageNumber,
                PageSize = filter.PageSize,
                RowCount = totalCount,
                PageCount = (int)Math.Ceiling(totalCount / (double)filter.PageSize)
            };
        }
        #region Private Helpers
        private async Task<List<Classroom>> FindAvailableClassrooms(CourseSectionConstraints constraints, ScheduleConstraintsDTO globalConstraints)
        {
            return await _dbReadOnlyContext.Classrooms
                .Where(c => c.Capacity >= constraints.RequiredCapacity &&
                           !_dbReadOnlyContext.Schedules.Any(s =>
                               s.ClassroomId == c.Id &&
                               (constraints.PreferredDays.Contains(s.DayOfWeek)) &&
                               s.StartTime < constraints.PreferredEndTime &&
                               s.EndTime > constraints.PreferredStartTime))
                .ToListAsync();
        }
        private static ScheduleDTO MapToDto(Schedule s) => new()
        {
            Id = s.Id,
            CourseId = s.CourseId,
            ClassroomId = s.ClassroomId,
            TeacherId = s.TeacherId,
            DayOfWeek = s.DayOfWeek,
            StartTime = s.StartTime,
            EndTime = s.EndTime,
            Duration  = s.Duration,
            GradeSectionId = s.GradeSectionId
        };

        private static Schedule MapToEntity(ScheduleDTO dto) => new()
        {
            Id = Guid.NewGuid(),
            CourseId = dto.CourseId,
            ClassroomId = dto.ClassroomId,
            TeacherId = dto.TeacherId,
            DayOfWeek = dto.DayOfWeek,
            StartTime = dto.StartTime,
            EndTime = dto.EndTime,
            GradeSectionId = dto.GradeSectionId
        };

        #endregion
    }

}
