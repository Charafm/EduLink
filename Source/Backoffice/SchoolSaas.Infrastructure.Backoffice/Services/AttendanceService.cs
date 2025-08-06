using Microsoft.EntityFrameworkCore;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Backoffice.Academic;
using SchoolSaas.Domain.Backoffice.Traceability;
using SchoolSaas.Domain.Common.Enums;
using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Common.DataObjects.Attendance;
using SchoolSaas.Domain.Common.DataObjects.Common;

namespace SchoolSaas.Infrastructure.Backoffice.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IBackofficeContext _dbContext;
        private readonly IBackofficeReadOnlyContext _dbReadOnlyContext;
        private readonly IServiceHelper _serviceHelper;

        public AttendanceService(
            IBackofficeContext dbContext,
            IBackofficeReadOnlyContext dbReadOnlyContext,
            IServiceHelper serviceHelper)
        {
            _dbContext = dbContext;
            _dbReadOnlyContext = dbReadOnlyContext;
            _serviceHelper = serviceHelper;
        }

        public async Task<bool> RecordAttendanceAsync(AttendanceDTO data, CancellationToken cancellationToken)
        {
            return await _serviceHelper.ExecuteWithTransactionAsync(async () =>
            {
                var attendance = MapToEntity(data);
                await _dbContext.Attendances.AddAsync(attendance, cancellationToken);
                //await CreateHistory(attendance, cancellationToken, AttendanceChangeReasonEnum.NewRecord);
                return true;
            });
        }

        public async Task<bool> UpdateAttendanceAsync(Guid attendanceId, AttendanceDTO data,
            AttendanceChangeReasonEnum reason, CancellationToken cancellationToken)
        {
            return await _serviceHelper.ExecuteWithTransactionAsync(async () =>
            {
                var attendance = await GetAttendanceEntity(attendanceId, cancellationToken);
                var originalValues = CaptureOriginalValues(attendance);

                UpdateEntity(attendance, data);
                //await CreateHistory(attendance, cancellationToken, reason, originalValues);

                return true;
            });
        }
        public async Task<bool> DeleteAttendanceAsync(Guid attendanceId, CancellationToken cancellationToken)
        {
            return await _serviceHelper.ExecuteWithTransactionAsync(async () =>
            {
                var attendance = await _dbContext.Attendances
                    .FirstOrDefaultAsync(a => a.Id == attendanceId, cancellationToken);

                if (attendance == null)
                    throw new KeyNotFoundException("Attendance record not found");

                _dbContext.Attendances.Remove(attendance);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return true;
            });
        }

        public async Task<List<AttendanceDTO>> GetAttendanceRecordsAsync(Guid studentId,
            CancellationToken cancellationToken)
        {
            return await _dbReadOnlyContext.Attendances
                .AsNoTracking()
                .Where(a => a.StudentId == studentId)
                .OrderByDescending(a => a.Date)
                .Select(a => MapToDto(a))
                .ToListAsync(cancellationToken);
        }

        public async Task<PagedResult<AttendanceHistoryDTO>> GetPaginatedAttendanceHistoryAsync(
            AttendanceHistoryFilterDTO filter, CancellationToken cancellationToken)
        {
            var query = BuildHistoryQuery(filter);

            var totalCount = await query.CountAsync(cancellationToken);
            var results = await PaginateResults(query, filter)
                .Select(h => MapHistoryToDto(h))
                .ToListAsync(cancellationToken);

            return CreatePagedResult(results, totalCount, filter);
        }
        public async Task<AttendanceDTO> GetAttendanceByIdAsync(Guid attendanceId, CancellationToken cancellationToken)
        {
            var attendance = await _dbReadOnlyContext.Attendances
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == attendanceId, cancellationToken);

            if (attendance == null)
                throw new KeyNotFoundException("Attendance record not found");

            return MapToDto(attendance);
        }

        public async Task<PagedResult<AttendanceDTO>> GetPaginatedAttendanceRecordsAsync(AttendanceFilterDTO filter, CancellationToken cancellationToken)
        {
            var query = _dbReadOnlyContext.Attendances.AsNoTracking();

            if (filter.StudentId.HasValue)
                query = query.Where(a => a.StudentId == filter.StudentId);

            if (filter.CourseId.HasValue)
                query = query.Where(a => a.CourseId == filter.CourseId);

            if (filter.FromDate.HasValue)
                query = query.Where(a => a.Date >= filter.FromDate.Value);

            if (filter.ToDate.HasValue)
                query = query.Where(a => a.Date <= filter.ToDate.Value);

            if (filter.Status.HasValue)
                query = query.Where(a => a.Status == filter.Status);

            var totalCount = await query.CountAsync(cancellationToken);
            var records = await query
                .OrderByDescending(a => a.Date)
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync(cancellationToken);

            return new PagedResult<AttendanceDTO>
            {
                Results = records.Select(MapToDto).ToList(),
                RowCount = totalCount,
                CurrentPage = filter.PageNumber,
                PageSize = filter.PageSize,
                PageCount = (int)Math.Ceiling(totalCount / (double)filter.PageSize)
            };
        }

        public async Task<bool> BulkRecordAttendanceAsync(BulkAttendanceDTO dto,
            CancellationToken cancellationToken)
        {
            return await _serviceHelper.ExecuteWithTransactionAsync(async () =>
            {
                var attendances = dto.Records.Select(MapToEntity).ToList();
                await _dbContext.Attendances.AddRangeAsync(attendances, cancellationToken);
               // await CreateBulkHistory(attendances);
                return true;
            });
        }

        public async Task<AttendanceSummaryDTO> GetAttendanceSummaryAsync(Guid studentId,
            DateRangeDTO dateRange, CancellationToken cancellationToken)
        {
            var query = _dbReadOnlyContext.Attendances
                .Where(a => a.StudentId == studentId &&
                           a.Date >= dateRange.StartDate &&
                           a.Date <= dateRange.EndDate);

            var totalDays = await query.CountAsync(cancellationToken);
            var presentDays = await query.CountAsync(a => a.Status == AttendanceEnum.Present, cancellationToken);
            var lateDays = await query.CountAsync(a => a.Status == AttendanceEnum.Late, cancellationToken);
            var absentDays = totalDays - presentDays - lateDays;

            return new AttendanceSummaryDTO
            {
                TotalDays = totalDays,
                PresentDays = presentDays,
                LateDays = lateDays,
                AbsentDays = absentDays,
                AttendanceRate = totalDays > 0 ?
                    Math.Round((decimal)(presentDays + lateDays) / totalDays * 100, 2) : 0
            };
        }

        public async Task<bool> SubmitAttendanceExcuseAsync(Guid attendanceId, AttendanceExcuseDTO dto,
            CancellationToken cancellationToken)
        {
            return await _serviceHelper.ExecuteWithTransactionAsync(async () =>
            {
                var attendance = await GetAttendanceEntity(attendanceId, cancellationToken);
                var originalValues = CaptureOriginalValues(attendance);

                UpdateNotes(attendance, dto);
                attendance.Status = AttendanceEnum.Absent;

               // await CreateHistory(attendance, cancellationToken, AttendanceChangeReasonEnum.AbscenseJustified, originalValues);
                return true;
            });
        }

        #region Private Helpers

        private async Task<Attendance> GetAttendanceEntity(Guid attendanceId, CancellationToken ct)
        {
            return await _dbContext.Attendances
                       .FirstOrDefaultAsync(a => a.Id == attendanceId, ct)
                   ?? throw new KeyNotFoundException("Attendance record not found");
        }

        private IQueryable<AttendanceHistory> BuildHistoryQuery(AttendanceHistoryFilterDTO filter)
        {
            var query = _dbReadOnlyContext.AttendanceHistories
                .AsNoTracking()
                .AsQueryable();

            if (filter.StudentId.HasValue)
                query = query.Where(h => h.StudentId == filter.StudentId.Value);

            if (filter.DateFrom.HasValue)
                query = query.Where(h => h.Date >= filter.DateFrom.Value);

            if (filter.DateTo.HasValue)
                query = query.Where(h => h.Date <= filter.DateTo.Value);

            if (filter.ChangeReason.HasValue)
                query = query.Where(h => h.ChangeReason == filter.ChangeReason.Value);

            return query.OrderByDescending(h => h.Date);
        }

        private async Task CreateBulkHistory(List<Attendance> attendances)
        {
            var histories = attendances.Select(a => new AttendanceHistory
            {
                AttendanceId = a.Id,
                StudentId = a.StudentId,
                Date = a.Date,
                Status = a.Status,
                ChangedBy = a.CreatedBy,
                ChangedAt = DateTime.UtcNow,
                ChangeReason = AttendanceChangeReasonEnum.NewRecord
            });

            await _dbContext.AttendanceHistories.AddRangeAsync(histories);
        }

        private async Task CreateHistory(Attendance attendance, CancellationToken cancellationToken,
            AttendanceChangeReasonEnum reason,
            AttendanceOriginalValues originalValues = null)
        {
            var history = new AttendanceHistory
            {
                AttendanceId = attendance.Id,
                StudentId = attendance.StudentId,
                Date = attendance.Date,
                Status = originalValues?.Status ?? attendance.Status,
                ChangedBy = attendance.LastModifiedBy ?? attendance.CreatedBy,
                ChangedAt = DateTime.UtcNow,
                ChangeReason = reason,
                PreviousNotesFr = originalValues?.NotesFr,
                PreviousNotesAr = originalValues?.NotesAr,
                PreviousNotesEn = originalValues?.NotesEn
            };

            await _dbContext.AttendanceHistories.AddAsync(history);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        private static AttendanceOriginalValues CaptureOriginalValues(Attendance attendance)
        {
            return new AttendanceOriginalValues
            {
                Status = attendance.Status,
                NotesFr = attendance.NotesFr,
                NotesAr = attendance.NotesAr,
                NotesEn = attendance.NotesEn
            };
        }

        private static void UpdateEntity(Attendance entity, AttendanceDTO dto)
        {
            entity.Status = dto.Status;
            entity.NotesFr = dto.NotesFr;
            entity.NotesAr = dto.NotesAr;
            entity.NotesEn = dto.NotesEn;
            entity.LastModified = DateTime.UtcNow;
        }

        private static void UpdateNotes(Attendance entity, AttendanceExcuseDTO dto)
        {
            switch (dto.Language.ToLower())
            {
                case "fr":
                    entity.NotesFr = dto.Explanation;
                    break;
                case "ar":
                    entity.NotesAr = dto.Explanation;
                    break;
                default:
                    entity.NotesEn = dto.Explanation;
                    break;
            }
        }

        private static AttendanceDTO MapToDto(Attendance entity) => new()
        {
            StudentId = entity.StudentId,
            CourseId = entity.CourseId,
            Date = entity.Date,
            Status = entity.Status,
            NotesFr = entity.NotesFr,
            NotesAr = entity.NotesAr,
            NotesEn = entity.NotesEn
        };

        private static AttendanceHistoryDTO MapHistoryToDto(AttendanceHistory entity) => new()
        {
            AttendanceId = entity.AttendanceId,
            StudentId = entity.StudentId,
            Date = entity.Date,
            Status = entity.Status,
            ChangedBy = entity.ChangedBy,
            ChangedAt = entity.ChangedAt,
            ChangeReason = entity.ChangeReason
        };

        private static Attendance MapToEntity(AttendanceDTO dto) => new()
        {
            StudentId = dto.StudentId,
            CourseId = dto.CourseId,
            Date = dto.Date,
            Status = dto.Status,
            NotesFr = dto.NotesFr,
            NotesAr = dto.NotesAr,
            NotesEn = dto.NotesEn
        };

        private static PagedResult<AttendanceHistoryDTO> CreatePagedResult(
            List<AttendanceHistoryDTO> items,
            int totalCount,
            AttendanceHistoryFilterDTO filter) => new()
            {
                Results = items,
                CurrentPage = filter.PageNumber,
                PageSize = filter.PageSize,
                RowCount = totalCount,
                PageCount = (int)Math.Ceiling(totalCount / (double)filter.PageSize)
            };

        private static IQueryable<AttendanceHistory> PaginateResults(
            IQueryable<AttendanceHistory> query,
            AttendanceHistoryFilterDTO filter) => query
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize);

        private class AttendanceOriginalValues
        {
            public AttendanceEnum Status { get; set; }
            public string NotesFr { get; set; }
            public string NotesAr { get; set; }
            public string NotesEn { get; set; }
        }

        #endregion
    }
}