using Microsoft.EntityFrameworkCore;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Backoffice.Academic;
using SchoolSaas.Domain.Backoffice.Traceability;
using SchoolSaas.Domain.Common.Enums;
using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Common.DataObjects.Academic;
using SchoolSaas.Domain.Common.DataObjects.Grade;

namespace SchoolSaas.Infrastructure.Backoffice.Services
{
    public class GradebookService : IGradebookService
    {
        private readonly IBackofficeContext _dbContext;
        private readonly IBackofficeReadOnlyContext _dbReadOnlyContext;
        private readonly IServiceHelper _serviceHelper;

        public GradebookService(
            IBackofficeContext dbContext,
            IBackofficeReadOnlyContext dbReadOnlyContext,
            IServiceHelper serviceHelper)
        {
            _dbContext = dbContext;
            _dbReadOnlyContext = dbReadOnlyContext;
            _serviceHelper = serviceHelper;
        }

        public async Task<bool> RecordGradeAsync(GradeDTO dto, CancellationToken cancellationToken)
        {
            return await _serviceHelper.ExecuteWithTransactionAsync(async () =>
            {
                var grade = MapToEntity(dto);
                await _dbContext.Grades.AddAsync(grade, cancellationToken);
                //await LogStatusChange(grade, cancellationToken, isNew: true);
                return true;
            });
        }

        public async Task<bool> UpdateGradeAsync(Guid gradeId, GradeUpdateDTO dto, CancellationToken cancellationToken)
        {
            return await _serviceHelper.ExecuteWithTransactionAsync(async () =>
            {
                var grade = await GetGradeEntity(gradeId, cancellationToken);
                var originalValues = CaptureOriginalValues(grade);

                UpdateGradeEntity(grade, dto);
                //await LogStatusChange(grade, cancellationToken, originalValues);
                return true;
            });
        }

        public async Task<PagedResult<GradeDTO>> GetPaginatedGradesAsync(GradeFilterDTO filter, CancellationToken cancellationToken)
        {
            var query = BuildGradeQuery(filter);
            var totalCount = await query.CountAsync(cancellationToken);
            var results = await PaginateResults(query, filter)
                .Select(g => MapToDto(g))
                .ToListAsync(cancellationToken);

            return CreatePagedResult(results, totalCount, filter);
        }

        public async Task<bool> BulkRecordGradesAsync(BulkGradeDTO dto, CancellationToken cancellationToken)
        {
            return await _serviceHelper.ExecuteWithTransactionAsync(async () =>
            {
                var grades = dto.Records.Select(MapToEntity).ToList();
                await _dbContext.Grades.AddRangeAsync(grades, cancellationToken);
                await LogBulkHistory(grades);
                return true;
            });
        }

        public async Task<bool> BulkUpdateGradesAsync(BulkGradeUpdateDTO dto, CancellationToken cancellationToken)
        {
            return await _serviceHelper.ExecuteWithTransactionAsync(async () =>
            {
                var grades = await _dbContext.Grades
                    .Where(g => dto.GradeIds.Contains(g.Id))
                    .ToListAsync(cancellationToken);

                foreach (var grade in grades)
                {
                    var originalValues = CaptureOriginalValues(grade);
                    UpdateGradeEntity(grade, dto.UpdateData);
                    //await LogStatusChange(grade, cancellationToken, originalValues);
                }

                await _dbContext.SaveChangesAsync(cancellationToken);
                return true;
            });
        }

        public async Task<GradeStatisticsDTO> GetGradeStatisticsAsync(GradeFilterDTO filter, CancellationToken cancellationToken)
        {
            var query = _dbReadOnlyContext.Grades
                .Where(g => g.CourseId == filter.CourseId && g.SemesterId == filter.SemesterId);

            return new GradeStatisticsDTO
            {
                AverageScore = await query.AverageAsync(g => g.Score, cancellationToken),
                MaxScore = await query.MaxAsync(g => g.Score, cancellationToken),
                MinScore = await query.MinAsync(g => g.Score, cancellationToken),
                GradeDistribution = await query
                   .GroupBy(g => g.GradeType)
                   .Select(g => new GradeDistributionDTO
                   {
                       GradeType = g.Key,
                       Count = g.Count(),
                       Average = g.Average(x => x.Score)
                   })
                   .ToListAsync(cancellationToken)
            };
        }

        public async Task<bool> SubmitGradeAppealAsync(GradeAppealDTO dto, CancellationToken cancellationToken)
        {
            return await _serviceHelper.ExecuteWithTransactionAsync(async () =>
            {
                var appeal = new GradeAppeal
                {
                    Id = Guid.NewGuid(),
                    GradeId = dto.GradeId,
                    Reason = dto.Reason,
                    Status = AppealStatusEnum.Pending,
                    SubmittedBy = dto.SubmittedBy,
                    SubmittedAt = DateTime.UtcNow
                };

                await _dbContext.GradeAppeals.AddAsync(appeal, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return true;
            });
        }

        public async Task<PagedResult<GradeAppealDTO>> GetGradeAppealsAsync(GradeAppealFilterDTO filter, CancellationToken cancellationToken)
        {
            var query = _dbReadOnlyContext.GradeAppeals
                .Include(a => a.Grade)
                .AsQueryable();

            if (filter.Status.HasValue)
                query = query.Where(a => a.Status == filter.Status.Value);

            var totalCount = await query.CountAsync(cancellationToken);
            var results = await query
                .OrderByDescending(a => a.SubmittedAt)
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .Select(a => new GradeAppealDTO
                {
                    GradeId = a.GradeId,
                    Reason = a.Reason,
                    Status = a.Status,
                    SubmittedBy = a.SubmittedBy,
                    SubmittedAt = a.SubmittedAt
                })
                .ToListAsync(cancellationToken);

            return new PagedResult<GradeAppealDTO>
            {
                Results = results,
                CurrentPage = filter.PageNumber,
                PageSize = filter.PageSize,
                RowCount = totalCount,
                PageCount = (int)Math.Ceiling(totalCount / (double)filter.PageSize)
            };
        }

        public async Task<PagedResult<GradeHistoryDTO>> GetGradeHistoryAsync(GradeHistoryFilterDTO filter, CancellationToken cancellationToken)
        {
            var query = _dbReadOnlyContext.GradeHistory
                .Where(h => h.GradeId == filter.GradeId)
                .OrderByDescending(h => h.ModifiedAt);

            var totalCount = await query.CountAsync(cancellationToken);
            var results = await query
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .Select(h => new GradeHistoryDTO
                {
                    GradeId = h.GradeId,
                    OldScore = h.OldScore,
                    NewScore = h.NewScore,
                    ModifiedBy = h.ModifiedBy,
                    ModifiedAt = h.ModifiedAt
                })
                .ToListAsync(cancellationToken);

            return new PagedResult<GradeHistoryDTO>
            {
                Results = results,
                CurrentPage = filter.PageNumber,
                PageSize = filter.PageSize,
                RowCount = totalCount,
                PageCount = (int)Math.Ceiling(totalCount / (double)filter.PageSize)
            };
        }

        public async Task<bool> CalculateFinalGradesAsync(Guid courseId, Guid semesterId, CancellationToken cancellationToken)
        {
            return await _serviceHelper.ExecuteWithTransactionAsync(async () =>
            {
                var grades = await _dbContext.Grades
                    .Where(g => g.CourseId == courseId && g.SemesterId == semesterId)
                    .ToListAsync(cancellationToken);

                // TODO: Final grade calculation logic goes here.
                // For instance, compute a weighted average based on grade types.

                await _dbContext.SaveChangesAsync(cancellationToken);
                return true;
            });
        }

        // NEW: Calculate GPA for a student (using all graded courses)
        public async Task<GpaDTO> CalculateGPAForStudentAsync(Guid studentId, CancellationToken cancellationToken)
        {
            // Retrieve all grades for the student
            var grades = await _dbReadOnlyContext.Grades
                .AsNoTracking()
                .Where(g => g.StudentId == studentId)
                .ToListAsync(cancellationToken);

            if (grades == null || !grades.Any())
                throw new InvalidOperationException("No grades found for the student.");

            // Basic GPA calculation: Sum of scores divided by number of grades
            var gpa = grades.Average(g => g.Score);

            return new GpaDTO
            {
                StudentId = studentId,
                GPA = Math.Round((decimal)gpa, 2)
            };
        }

        // NEW: Retrieve paginated grades submitted by a specific teacher
        public async Task<PagedResult<GradeDTO>> GetGradesByTeacherAsync(Guid teacherId, GradeFilterDTO filter, CancellationToken cancellationToken)
        {
            var query = _dbReadOnlyContext.Grades
                .AsNoTracking()
                .Where(g => g.TeacherId == teacherId);  // Assuming there's a TeacherId in Grade entity

            if (filter.StudentId.HasValue)
                query = query.Where(g => g.StudentId == filter.StudentId.Value);
            if (filter.CourseId.HasValue)
                query = query.Where(g => g.CourseId == filter.CourseId.Value);
            if (filter.SemesterId.HasValue)
                query = query.Where(g => g.SemesterId == filter.SemesterId.Value);
            if (filter.MinScore.HasValue)
                query = query.Where(g => g.Score >= filter.MinScore.Value);
            if (filter.MaxScore.HasValue)
                query = query.Where(g => g.Score <= filter.MaxScore.Value);

            var totalCount = await query.CountAsync(cancellationToken);
            var results = await query
                .OrderByDescending(g => g.Created)
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync(cancellationToken);

            var gradeDtos = results.Select(g => MapToDto(g)).ToList();

            return new PagedResult<GradeDTO>
            {
                Results = gradeDtos,
                CurrentPage = filter.PageNumber,
                PageSize = filter.PageSize,
                RowCount = totalCount,
                PageCount = (int)Math.Ceiling(totalCount / (double)filter.PageSize)
            };
        }

        #region Private Helpers

        private async Task<Grade> GetGradeEntity(Guid gradeId, CancellationToken ct)
        {
            return await _dbContext.Grades.FirstOrDefaultAsync(g => g.Id == gradeId, ct)
                   ?? throw new KeyNotFoundException("Grade not found");
        }

        private IQueryable<Grade> BuildGradeQuery(GradeFilterDTO filter)
        {
            var query = _dbReadOnlyContext.Grades
                .Include(g => g.Student)
                .Include(g => g.Course)
                .AsQueryable();

            if (filter.StudentId.HasValue)
                query = query.Where(g => g.StudentId == filter.StudentId.Value);

            if (filter.CourseId.HasValue)
                query = query.Where(g => g.CourseId == filter.CourseId.Value);

            if (filter.SemesterId.HasValue)
                query = query.Where(g => g.SemesterId == filter.SemesterId.Value);

            if (filter.MinScore.HasValue)
                query = query.Where(g => g.Score >= filter.MinScore.Value);

            if (filter.MaxScore.HasValue)
                query = query.Where(g => g.Score <= filter.MaxScore.Value);

            return query.OrderByDescending(g => g.Created);
        }

        private async Task LogGradeHistory(Grade grade, CancellationToken cancellationToken, GradeOriginalValues original = null, bool isNew = false)
        {
            var history = new GradeHistory
            {
                Id = Guid.NewGuid(),
                GradeId = grade.Id,
                OldScore = isNew ? 0 : original?.Score ?? grade.Score,
                NewScore = grade.Score,
                ModifiedBy = grade.LastModifiedBy ?? "system",
                ModifiedAt = DateTime.UtcNow,
                OldComments = isNew ? null : original?.Comments.ToString(),
                NewComments = GetCurrentComments(grade).ToString()
            };

            await _dbContext.GradeHistory.AddAsync(history, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        private async Task LogBulkHistory(List<Grade> grades)
        {
            var histories = grades.Select(g => new GradeHistory
            {
                Id = Guid.NewGuid(),
                GradeId = g.Id,
                OldScore = 0,
                NewScore = g.Score,
                ModifiedBy = g.CreatedBy,
                ModifiedAt = DateTime.UtcNow
            });

            await _dbContext.GradeHistory.AddRangeAsync(histories);
        }

        private static GradeOriginalValues CaptureOriginalValues(Grade grade)
        {
            return new GradeOriginalValues
            {
                Score = grade.Score,
                Comments = new GradeComments
                {
                    Fr = grade.TeacherCommentFr,
                    Ar = grade.TeacherCommentAr,
                    En = grade.TeacherCommentEn
                }
            };
        }

        private static GradeComments GetCurrentComments(Grade grade) => new GradeComments
        {
            Fr = grade.TeacherCommentFr,
            Ar = grade.TeacherCommentAr,
            En = grade.TeacherCommentEn
        };

        private static void UpdateGradeEntity(Grade entity, GradeUpdateDTO dto)
        {
            entity.Score = dto.Score;
            entity.GradeType = dto.GradeType;
            entity.TeacherCommentFr = dto.TeacherCommentFr;
            entity.TeacherCommentAr = dto.TeacherCommentAr;
            entity.TeacherCommentEn = dto.TeacherCommentEn;
            entity.LastModified = DateTime.UtcNow;
        }

        private static Grade MapToEntity(GradeDTO dto) => new Grade
        {
            // Assume Id is generated later if needed
            StudentId = dto.StudentId,
            CourseId = dto.CourseId,
            SemesterId = dto.SemesterId,
            Score = dto.Score,
            GradeType = dto.GradeType,
            TeacherCommentFr = dto.TeacherCommentFr,
            TeacherCommentAr = dto.TeacherCommentAr,
            TeacherCommentEn = dto.TeacherCommentEn
        };

        private static GradeDTO MapToDto(Grade grade) => new GradeDTO
        {
            Id = grade.Id,
            StudentId = grade.StudentId,
            CourseId = grade.CourseId,
            SemesterId = grade.SemesterId,
            Score = grade.Score,
            GradeType = grade.GradeType,
            TeacherCommentFr = grade.TeacherCommentFr,
            TeacherCommentAr = grade.TeacherCommentAr,
            TeacherCommentEn = grade.TeacherCommentEn
        };

        private static PagedResult<GradeDTO> CreatePagedResult(
            List<GradeDTO> items,
            int totalCount,
            GradeFilterDTO filter) => new PagedResult<GradeDTO>
            {
                Results = items,
                CurrentPage = filter.PageNumber,
                PageSize = filter.PageSize,
                RowCount = totalCount,
                PageCount = (int)Math.Ceiling(totalCount / (double)filter.PageSize)
            };

        private static IQueryable<Grade> PaginateResults(
            IQueryable<Grade> query,
            GradeFilterDTO filter) => query
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize);

        private class GradeOriginalValues
        {
            public double Score { get; set; }
            public GradeComments Comments { get; set; }
        }

        private class GradeComments
        {
            public string Fr { get; set; }
            public string Ar { get; set; }
            public string En { get; set; }

            public override string ToString() => $"FR:{Fr}, AR:{Ar}, EN:{En}";
        }

        #endregion
    }
}
