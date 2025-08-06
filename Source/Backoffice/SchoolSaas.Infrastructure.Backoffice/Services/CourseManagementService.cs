using Microsoft.EntityFrameworkCore;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Backoffice.Academic;
using SchoolSaas.Domain.Common.DataObjects.Course;
using SchoolSaas.Domain.Common.DataObjects.Grade;
using SchoolSaas.Domain.Common.DataObjects.Teacher;

namespace SchoolSaas.Infrastructure.Backoffice.Services
{
    public class CourseManagementService : ICourseManagementService
    {
        private readonly IBackofficeContext _dbContext;
        private readonly IBackofficeReadOnlyContext _dbReadOnlyContext;
        private readonly IServiceHelper _serviceHelper;

        public CourseManagementService(
            IBackofficeContext dbContext,
            IBackofficeReadOnlyContext dbReadOnlyContext,
            IServiceHelper serviceHelper)
        {
            _dbContext = dbContext;
            _dbReadOnlyContext = dbReadOnlyContext;
            _serviceHelper = serviceHelper;
        }

        public async Task<bool> CreateCourseAsync(CourseDTO dto, CancellationToken cancellationToken)
        {
            return await _serviceHelper.ExecuteWithTransactionAsync(async () =>
            {
                var course = new Course
                {
                    TitleFr = dto.TitleFr,
                    TitleAr = dto.TitleAr,
                    TitleEn = dto.TitleEn,
                    Code = dto.Code,
                    Description = dto.Description
                };

                await _dbContext.Courses.AddAsync(course, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return true;
            });
        }
        public async Task<CourseDetailDTO> GetCourseDetailsAsync(Guid courseId, CancellationToken cancellationToken)
        {
            // Get basic course info
            var course = await _dbReadOnlyContext.Courses
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == courseId, cancellationToken)
                ?? throw new KeyNotFoundException("Course not found.");

            // Retrieve teacher assignments for the course (including teacher details if available)
            var teacherAssignments = await _dbReadOnlyContext.TeacherCourseAssignments
                .Where(a => a.CourseId == courseId)
                .Include(a => a.Teacher) // Assuming Teacher navigation property exists
                .Select(a => new TeacherAssignmentDetailDTO
                {
                    TeacherId = a.TeacherId,
                    TeacherName = $"{a.Teacher.FirstNameFr} {a.Teacher.LastNameFr}", // Adjust property names accordingly
                    AcademicYearId = a.AcademicYearId
                })
                .ToListAsync(cancellationToken);

            // Retrieve grade level mappings for the course
            var gradeMappings = await _dbReadOnlyContext.CourseGradeMappings
                .Where(m => m.CourseId == courseId)
                .Include(m => m.GradeLevel)
                .Select(m => new CourseGradeMappingDTO
                {
                    CourseId = m.CourseId,
                    GradeLevelId = m.GradeLevelId,
                   
                    IsElective = m.IsElective
                })
                .ToListAsync(cancellationToken);

            // Retrieve course materials if supported.
            // This assumes there is an entity called "CourseMaterial" in your _dbContext.
            //var materials = await _dbReadOnlyContext.CourseMaterials
            //    .Where(m => m.CourseId == courseId)
            //    .Select(m => new CourseMaterialDTO
            //    {
            //        Id = m.Id,
            //        Title = m.Title,
            //        Content = m.Content,
            //        LastModified = m.LastModified
            //    })
            //    .ToListAsync(cancellationToken);

            return new CourseDetailDTO
            {
                // Basic course data
                TitleFr = course.TitleFr,
                TitleAr = course.TitleAr,
                TitleEn = course.TitleEn,
                Code = course.Code,
                Description = course.Description,

                // Detailed data
                TeacherAssignments = teacherAssignments,
                GradeMappings = gradeMappings,
                //Materials = materials
            };
        }
        public async Task<bool> UpdateCourseAsync(Guid courseId, CourseDTO dto, CancellationToken cancellationToken)
        {
            return await _serviceHelper.ExecuteWithTransactionAsync(async () =>
            {
                var course = await _dbContext.Courses
                    .FirstOrDefaultAsync(c => c.Id == courseId, cancellationToken)
                    ?? throw new KeyNotFoundException("Course not found");

                course.TitleFr = dto.TitleFr;
                course.TitleAr = dto.TitleAr;
                course.TitleEn = dto.TitleEn;
                course.Code = dto.Code;
                course.Description = dto.Description;

                await _dbContext.SaveChangesAsync(cancellationToken);
                return true;
            });
        }
        public async Task<CourseDTO> GetCourseByIdAsync(Guid courseId, CancellationToken cancellationToken)
        {
            var course = await _dbReadOnlyContext.Courses
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == courseId, cancellationToken)
                ?? throw new KeyNotFoundException("Course not found");

            return MapToDto(course);
        }
        public async Task<bool> DeleteCourseAsync(Guid courseId, CancellationToken cancellationToken)
        {
            return await _serviceHelper.ExecuteWithTransactionAsync(async () =>
            {
                var course = await _dbContext.Courses
                    .FirstOrDefaultAsync(c => c.Id == courseId, cancellationToken)
                    ?? throw new KeyNotFoundException("Course not found");

                _dbContext.Courses.Remove(course);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return true;
            });
        }
        public async Task<PagedResult<CourseDTO>> GetCoursesAssignedToTeacherAsync(Guid teacherId, int page, int? size, CancellationToken cancellationToken)
        {
            var query = _dbReadOnlyContext.TeacherCourseAssignments
                .Where(a => a.TeacherId == teacherId)
                .Include(a => a.Course)
                .Select(a => a.Course);

            var total = await query.CountAsync(cancellationToken);
            var results = await query
                .Skip(((page - 1) * (size?? PagedResult<CourseDTO>.DefaultPageSize)))
                .Take(size?? PagedResult<CourseDTO>.DefaultPageSize)
                .ToListAsync(cancellationToken);

            return new PagedResult<CourseDTO>
            {
                Results = results.Select(MapToDto).ToList(),
                RowCount = total,
                CurrentPage = page,
                PageSize = size?? PagedResult<CourseDTO>.DefaultPageSize,
                PageCount = (int)Math.Ceiling(total / (double)(size ?? PagedResult<CourseDTO>.DefaultPageSize)) 
            };
        }
        public async Task<bool> BulkUnassignTeachersFromCoursesAsync(
       BulkTeacherAssignmentDTO dto,
       CancellationToken cancellationToken)
        {
            return await _serviceHelper.ExecuteWithTransactionAsync(async () =>
            {
                var toRemove = await _dbContext.TeacherCourseAssignments
                    .Where(a => dto.Assignments.Any(x =>
                        x.TeacherId == a.TeacherId &&
                        x.CourseId == a.CourseId &&
                        x.AcademicYearId == a.AcademicYearId))
                    .ToListAsync(cancellationToken);

                if (toRemove.Any())
                {
                    _dbContext.TeacherCourseAssignments.RemoveRange(toRemove);
                    await _dbContext.SaveChangesAsync(cancellationToken);
                }

                return true;
            });
        }
        public async Task<List<CourseDTO>> SearchCoursesAsync(
       string term,
       CancellationToken cancellationToken)
        {
            var query = _dbReadOnlyContext.Courses
                .AsNoTracking();

            if (!string.IsNullOrWhiteSpace(term))
            {
                query = query.Where(c =>
                    c.TitleFr.Contains(term) ||
                    c.TitleAr.Contains(term) ||
                    c.TitleEn.Contains(term) ||
                    (c.Code != null && c.Code.Contains(term)));
            }

            var list = await query
                .OrderBy(c => c.TitleFr)
                .Select(c => MapToDto(c))
                .ToListAsync(cancellationToken);

            return list;
        }
        public async Task<bool> AssignTeacherToCourseAsync(Guid teacherId, Guid courseId, Guid academicYearId, CancellationToken cancellationToken)
        {
            return await _serviceHelper.ExecuteWithTransactionAsync(async () =>
            {
                if (await AssignmentExists(teacherId, courseId, academicYearId))
                    return false;

                await _dbContext.TeacherCourseAssignments.AddAsync(new TeacherCourseAssignment
                {
                    TeacherId = teacherId,
                    CourseId = courseId,
                    AcademicYearId = academicYearId
                }, cancellationToken);

                await _dbContext.SaveChangesAsync(cancellationToken);
                return true;
            });
        }
        public async Task<bool> UnassignTeacherFromCourseAsync(Guid teacherId, Guid courseId, Guid academicYearId, CancellationToken cancellationToken)
        {
            return await _serviceHelper.ExecuteWithTransactionAsync(async () =>
            {
                var assignment = await _dbContext.TeacherCourseAssignments
                    .FirstOrDefaultAsync(a =>
                        a.TeacherId == teacherId &&
                        a.CourseId == courseId &&
                        a.AcademicYearId == academicYearId,
                        cancellationToken);

                if (assignment == null)
                    return false;

                _dbContext.TeacherCourseAssignments.Remove(assignment);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return true;
            });
        }
        public async Task<List<CourseGradeMappingDTO>> GetCourseMappingsAsync(Guid courseId, CancellationToken cancellationToken)
        {
            return await _dbReadOnlyContext.CourseGradeMappings
                .Where(m => m.CourseId == courseId)
                .Include(m => m.GradeLevel)
                .Select(m => new CourseGradeMappingDTO
                {
                    CourseId = m.CourseId,
                    GradeLevelId = m.GradeLevelId,
                    IsElective = m.IsElective
                    
                })
                .ToListAsync(cancellationToken);
        }
        public async Task<bool> UnmapCourseFromGradeLevelAsync(Guid courseId, Guid gradeLevelId, CancellationToken cancellationToken)
        {
            return await _serviceHelper.ExecuteWithTransactionAsync(async () =>
            {
                var mapping = await _dbContext.CourseGradeMappings
                    .FirstOrDefaultAsync(m => m.CourseId == courseId && m.GradeLevelId == gradeLevelId, cancellationToken);

                if (mapping == null)
                    return false;

                _dbContext.CourseGradeMappings.Remove(mapping);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return true;
            });
        }

        public async Task<PagedResult<CourseDTO>> GetPaginatedCoursesAsync(CourseFilterDTO filter, CancellationToken cancellationToken)
        {
            var query = BuildCourseQuery(filter);

            var totalCount = await query.CountAsync(cancellationToken);
            var results = await PaginateResults(query, filter)
                .Select(c => MapToDto(c))
                .ToListAsync(cancellationToken);

            return CreatePagedResult(results, totalCount, filter);
        }

        public async Task<bool> BulkAssignTeachersToCoursesAsync(BulkTeacherAssignmentDTO dto, CancellationToken cancellationToken)
        {
            return await _serviceHelper.ExecuteWithTransactionAsync(async () =>
            {
                var existingAssignments = await GetExistingAssignments(dto.Assignments);
                var newAssignments = dto.Assignments
                    .Where(a => !existingAssignments.Contains((a.TeacherId, a.CourseId, a.AcademicYearId)))
                    .Select(a => new TeacherCourseAssignment
                    {
                        TeacherId = a.TeacherId,
                        CourseId = a.CourseId,
                        AcademicYearId = a.AcademicYearId
                    });

                await _dbContext.TeacherCourseAssignments.AddRangeAsync(newAssignments, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return true;
            });
        }
       
        public async Task<List<CourseDTO>> GetCoursesByGradeLevelAsync(Guid gradeLevelId, CancellationToken cancellationToken)
        {
            return await _dbReadOnlyContext.CourseGradeMappings
                .Where(m => m.GradeLevelId == gradeLevelId)
                .Select(m => MapToDto(m.Course))
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> MapCourseToGradeLevelAsync(CourseGradeMappingDTO dto, CancellationToken cancellationToken)
        {
            return await _serviceHelper.ExecuteWithTransactionAsync(async () =>
            {
                if (await MappingExists(dto.CourseId, dto.GradeLevelId))
                    return false;

                await _dbContext.CourseGradeMappings.AddAsync(new CourseGradeMapping
                {
                    CourseId = dto.CourseId,
                    GradeLevelId = dto.GradeLevelId,
                    IsElective = dto.IsElective
                }, cancellationToken);

                await _dbContext.SaveChangesAsync(cancellationToken);
                return true;
            });
        }

      

        #region Private Helpers

        private async Task<bool> AssignmentExists(Guid teacherId, Guid courseId, Guid academicYearId)
        {
            return await _dbContext.TeacherCourseAssignments
                .AnyAsync(a => a.TeacherId == teacherId &&
                             a.CourseId == courseId &&
                             a.AcademicYearId == academicYearId);
        }

        private async Task<HashSet<(Guid, Guid, Guid)>> GetExistingAssignments(IEnumerable<TeacherAssignmentDTO> assignments)
        {
            var teacherIds = assignments.Select(x => x.TeacherId).ToHashSet();
            var courseIds = assignments.Select(x => x.CourseId).ToHashSet();
            var yearIds = assignments.Select(x => x.AcademicYearId).ToHashSet();

            var query = await _dbContext.TeacherCourseAssignments
                .Where(a =>
                    teacherIds.Contains(a.TeacherId) &&
                    courseIds.Contains(a.CourseId) &&
                    yearIds.Contains(a.AcademicYearId))
                .Select(a => new
                {
                    a.TeacherId,
                    a.CourseId,
                    a.AcademicYearId
                })
                .ToListAsync();

            return query
                .Select(x => (x.TeacherId, x.CourseId, x.AcademicYearId))
                .ToHashSet();
        }

        private IQueryable<Course> BuildCourseQuery(CourseFilterDTO filter)
        {
            var query = _dbReadOnlyContext.Courses.AsQueryable();

            if (!string.IsNullOrEmpty(filter.SearchTerm))
            {
                query = query.Where(c =>
                    c.TitleFr.Contains(filter.SearchTerm) ||
                    c.Code.Contains(filter.SearchTerm));
            }

            if (filter.GradeLevelId.HasValue)
            {
                query = query.Join(_dbReadOnlyContext.CourseGradeMappings,
                    c => c.Id,
                    m => m.CourseId,
                    (c, m) => new { Course = c, Mapping = m })
                    .Where(x => x.Mapping.GradeLevelId == filter.GradeLevelId)
                    .Select(x => x.Course);
            }

            return query.OrderBy(c => c.TitleFr);
        }

        private static CourseDTO MapToDto(Course course) => new()
        {
            TitleFr = course.TitleFr,
            TitleAr = course.TitleAr,
            TitleEn = course.TitleEn,
            Code = course.Code,
            Description = course.Description
        };

      

        private static PagedResult<CourseDTO> CreatePagedResult(
            List<CourseDTO> items,
            int totalCount,
            CourseFilterDTO filter) => new()
            {
                Results = items,
                CurrentPage = filter.PageNumber,
                PageSize = filter.PageSize,
                RowCount = totalCount,
                PageCount = (int)Math.Ceiling(totalCount / (double)filter.PageSize)
            };

        private static IQueryable<Course> PaginateResults(
            IQueryable<Course> query,
            CourseFilterDTO filter) => query
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize);

        private async Task<bool> MappingExists(Guid courseId, Guid gradeLevelId)
        {
            return await _dbContext.CourseGradeMappings
                .AnyAsync(m => m.CourseId == courseId && m.GradeLevelId == gradeLevelId);
        }

        #endregion
    }
}