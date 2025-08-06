using Microsoft.EntityFrameworkCore;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Backoffice.Academic;
using SchoolSaas.Domain.Backoffice.Staff;
using SchoolSaas.Domain.Common.DataObjects.Course;
using SchoolSaas.Domain.Common.DataObjects.Schedule;
using SchoolSaas.Domain.Common.DataObjects.Teacher;
using SchoolSaas.Domain.Common.Enums;
using static SchoolSaas.Infrastructure.Backoffice.Services.StudentService;
using System.Text.Json;

namespace SchoolSaas.Infrastructure.Backoffice.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly IBackofficeContext _dbContext;
        private readonly IBackofficeReadOnlyContext _dbReadOnlyContext;
        private readonly IServiceHelper _serviceHelper;
        private readonly IHttpClientFactory _httpClientFactory;

        public TeacherService(
            IBackofficeContext dbContext,
            IBackofficeReadOnlyContext dbReadOnlyContext,
            IServiceHelper serviceHelper,             IHttpClientFactory httpClientFactory)
        {
            _dbContext = dbContext;
            _dbReadOnlyContext = dbReadOnlyContext;
            _serviceHelper = serviceHelper;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<CreateUserRequestDto> CreateTeacherAsync(CreateTeacherDTO dto, CancellationToken cancellationToken)
        {
            return await _serviceHelper.ExecuteWithTransactionAsync(async () =>
            {
                var httpClient = _httpClientFactory.CreateClient("EdulinkIdentityFrontal");
                var identityUrl = $"{httpClient.BaseAddress}identity/createstaffuser";

                var password = $"Edu{Guid.NewGuid().ToString("N").Substring(0, 8)}*";
                var userDto = new CreateUserRequestDto
                {
                    FirstNameFr = dto.FirstNameFr,
                    LastNameFr = dto.LastNameFr,
                    FirstNameAr = dto.FirstNameAr,
                    LastNameAr = dto.LastNameAr,
                    Email = dto.Email,
                    Phone = dto.Phone,
                    AddressFr = null,
                    Password = password
                };

                var jsonContent = new StringContent(
                    JsonSerializer.Serialize(userDto),
                    System.Text.Encoding.UTF8,
                    "application/json"
                );

                var response = await httpClient.PostAsync(identityUrl, jsonContent, cancellationToken);

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Identity creation failed: {error}");
                }
                var json = await response.Content.ReadAsStringAsync(cancellationToken);
                var identityUser = JsonSerializer.Deserialize<IdentityUserResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });


                var teacher = new Teacher
                {
                    Id = Guid.NewGuid(),
                    UserId = identityUser.Id.ToString(),
                    BranchId = dto.BranchId,
                    FirstNameFr = dto.FirstNameFr,
                    FirstNameAr = dto.FirstNameAr,
                    LastNameFr = dto.LastNameFr,
                    LastNameAr = dto.LastNameAr,
                    Phone = dto.Phone,
                    Email = dto.Email,
                    HireDate = dto.HireDate,
                    SpecializationFr = dto.SpecializationFr,
                    SpecializationAr = dto.SpecializationAr,
                    Status = dto.Status
                };

                await _dbContext.Teachers.AddAsync(teacher, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return userDto;
            });
        }

        public async Task<bool> UpdateTeacherAsync(Guid teacherId, TeacherDTO dto, CancellationToken cancellationToken)
        {
            return await _serviceHelper.ExecuteWithTransactionAsync(async () =>
            {
                var teacher = await _dbContext.Teachers.FirstOrDefaultAsync(t => t.Id == teacherId, cancellationToken);
                if (teacher == null)
                    throw new KeyNotFoundException("Teacher not found.");

                teacher.UserId = dto.UserId;
                teacher.BranchId = dto.BranchId;
                teacher.FirstNameFr = dto.FirstNameFr;
                teacher.FirstNameAr = dto.FirstNameAr;
                teacher.LastNameFr = dto.LastNameFr;
                teacher.LastNameAr = dto.LastNameAr;
                teacher.Phone = dto.Phone;
                teacher.Email = dto.Email;
                teacher.HireDate = dto.HireDate;
                teacher.SpecializationFr = dto.SpecializationFr;
                teacher.SpecializationAr = dto.SpecializationAr;
                teacher.Status = dto.Status;
                teacher.LastModified = DateTime.UtcNow;

                _dbContext.Teachers.Update(teacher);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return true;
            });
        }

        public async Task<TeacherDTO> GetTeacherByIdAsync(Guid teacherId, CancellationToken cancellationToken)
        {
            try
            {
                var teacher = await _dbReadOnlyContext.Teachers
                    .AsNoTracking()
                    .FirstOrDefaultAsync(t => t.Id == teacherId, cancellationToken);

                if (teacher == null)
                    throw new KeyNotFoundException("Teacher not found.");

                return new TeacherDTO
                {
                    UserId = teacher.UserId,
                    BranchId = teacher.BranchId,
                    FirstNameFr = teacher.FirstNameFr,
                    FirstNameAr = teacher.FirstNameAr,
                    LastNameFr = teacher.LastNameFr,
                    LastNameAr = teacher.LastNameAr,
                    Phone = teacher.Phone,
                    Email = teacher.Email,
                    HireDate = teacher.HireDate,
                    SpecializationFr = teacher.SpecializationFr,
                    SpecializationAr = teacher.SpecializationAr,
                    Status = teacher.Status
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public async Task<PagedResult<TeacherDTO>> GetPaginatedTeachersAsync(FilterTeacherDTO filter, CancellationToken cancellationToken)
        {
            try
            {
                var query = _dbReadOnlyContext.Teachers.AsNoTracking();

                // Apply filters
                if (filter.BranchId.HasValue)
                    query = query.Where(t => t.BranchId == filter.BranchId.Value);

                if (!string.IsNullOrEmpty(filter.Specialization))
                    query = query.Where(t => t.SpecializationFr.Contains(filter.Specialization) ||
                                             t.SpecializationAr.Contains(filter.Specialization));

                if (filter.Status.HasValue)
                    query = query.Where(t => t.Status == filter.Status.Value);

                // Apply sorting
                if (!string.IsNullOrEmpty(filter.SortBy))
                    query = ApplySorting(query, filter.SortBy, filter.IsAscending);
                else
                    query = query.OrderBy(t => t.LastNameFr);

                // Calculate total count before pagination
                var totalCount = await query.CountAsync(cancellationToken);

                // Apply pagination
                var teachers = await query
                    .Skip((filter.PageNumber - 1) * filter.PageSize ?? PagedResult<TeacherDTO>.DefaultPageSize)
                    .Take(filter.PageSize ?? PagedResult<TeacherDTO>.DefaultPageSize)
                    .ToListAsync(cancellationToken);

                // Map to DTOs
                var teacherDtos = teachers.Select(t => new TeacherDTO
                {
                    UserId = t.UserId,
                    BranchId = t.BranchId,
                    FirstNameFr = t.FirstNameFr,
                    FirstNameAr = t.FirstNameAr,
                    LastNameFr = t.LastNameFr,
                    LastNameAr = t.LastNameAr,
                    Phone = t.Phone,
                    Email = t.Email,
                    HireDate = t.HireDate,
                    SpecializationFr = t.SpecializationFr,
                    SpecializationAr = t.SpecializationAr,
                    Status = t.Status
                }).ToList();

                return new PagedResult<TeacherDTO>
                {
                    Results = teacherDtos,
                    RowCount = totalCount,
                    CurrentPage = filter.PageNumber ?? PagedResult<TeacherDTO>.DefaultPageSize,
                    PageSize = filter.PageSize ?? PagedResult<TeacherDTO>.DefaultPageSize,
                    PageCount = (int)Math.Ceiling((double)totalCount / filter.PageSize ?? PagedResult<TeacherDTO>.DefaultPageSize)
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new PagedResult<TeacherDTO>
                {
                    Results = new List<TeacherDTO>(),
                    RowCount = 0,
                    CurrentPage = filter.PageNumber ?? 1,
                    PageSize = filter.PageSize ?? PagedResult<TeacherDTO>.DefaultPageSize,
                    PageCount = 0
                };
            }
        }

        public async Task<bool> DeleteTeacherAsync(Guid teacherId, CancellationToken cancellationToken)
        {
            return await _serviceHelper.ExecuteWithTransactionAsync(async () =>
            {
                var teacher = await _dbContext.Teachers.FirstOrDefaultAsync(t => t.Id == teacherId, cancellationToken);
                if (teacher == null)
                    throw new KeyNotFoundException("Teacher not found.");

                teacher.IsDeleted = true;
                teacher.LastModified = DateTime.UtcNow;
                _dbContext.Teachers.Update(teacher);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return true;
            });
        }
        public async Task<TeacherDTO> GetTeacherByUserId(string userId)
        {
            var query = _dbReadOnlyContext.Teachers
               .AsNoTracking().FirstOrDefaultAsync(s => s.UserId == userId).Result;
            if (query == null)
            {
                throw new KeyNotFoundException("Teacher not found");
            }
            return new TeacherDTO
            {
                UserId = userId,
                BranchId = query.BranchId,
                FirstNameFr = query.FirstNameFr,
                FirstNameAr = query.FirstNameAr,
                LastNameFr = query.LastNameFr,
                LastNameAr = query.LastNameAr,
                Email = query.Email,
                Phone = query.Phone,
                HireDate = query.HireDate,
                SpecializationFr = query.SpecializationFr,
                SpecializationAr = query.SpecializationAr,
                Status = query.Status
            };
        }
        public async Task<bool> UpdateTeacherEmploymentStatusAsync(Guid teacherId, TeacherStatusEnum newStatus, CancellationToken cancellationToken)
        {
            return await _serviceHelper.ExecuteWithTransactionAsync(async () =>
            {
                var teacher = await _dbContext.Teachers.FirstOrDefaultAsync(t => t.Id == teacherId, cancellationToken);
                if (teacher == null)
                    throw new KeyNotFoundException("Teacher not found.");

                teacher.Status = newStatus;
                teacher.LastModified = DateTime.UtcNow;
                _dbContext.Teachers.Update(teacher);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return true;
            });
        }
        public async Task<bool> ReassignTeacherToCourseAsync(Guid teacherId, Guid courseId, CancellationToken cancellationToken)
        {
            return await _serviceHelper.ExecuteWithTransactionAsync(async () =>
            {
                var exists = await _dbContext.TeacherCourseAssignments
                    .AnyAsync(x => x.TeacherId == teacherId && x.CourseId == courseId && !(x.IsDeleted.HasValue), cancellationToken);

                if (exists)
                    return false;

                var assignment = new TeacherCourseAssignment
                {
                    Id = Guid.NewGuid(),
                    TeacherId = teacherId,
                    CourseId = courseId,
                    Created = DateTime.UtcNow
                };

                await _dbContext.TeacherCourseAssignments.AddAsync(assignment, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return true;
            });
        }
        public async Task<bool> UnassignTeacherFromCourseAsync(Guid teacherId, Guid courseId, CancellationToken cancellationToken)
        {
            return await _serviceHelper.ExecuteWithTransactionAsync(async () =>
            {
                var assignment = await _dbContext.TeacherCourseAssignments
                    .FirstOrDefaultAsync(x => x.TeacherId == teacherId && x.CourseId == courseId && !(x.IsDeleted.HasValue), cancellationToken);

                if (assignment == null)
                    return false;

                assignment.IsDeleted = true;
                assignment.LastModified = DateTime.UtcNow;

                _dbContext.TeacherCourseAssignments.Update(assignment);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return true;
            });
        }
        

        public async Task<List<CourseScheduleDTO>> GetTeacherAssignedCoursesAsync(Guid teacherId, CancellationToken cancellationToken)
        {
            try
            {
                // Retrieve course IDs assigned to this teacher via TeacherCourseAssignment
                var courseIds = await _dbReadOnlyContext.TeacherCourseAssignments
                    .AsNoTracking()
                    .Where(tc => tc.TeacherId == teacherId) // !tc.IsDeleted
                    .Select(tc => tc.CourseId)
                    .ToListAsync(cancellationToken);

                if (!courseIds.Any())
                    return new List<CourseScheduleDTO>();

                var result = new List<CourseScheduleDTO>();

                foreach (var courseId in courseIds)
                {
                    // Retrieve course details
                    var course = await _dbReadOnlyContext.Courses
                        .AsNoTracking()
                        //.Include(c => c.Grade) // Removed because Course does not include a Grade navigation property
                        .FirstOrDefaultAsync(c => c.Id == courseId, cancellationToken);
                    if (course == null)
                        continue;

                    // Retrieve schedule items for this course
                    var scheduleItems = await _dbReadOnlyContext.Schedules
                        .AsNoTracking()
                        .Include(s => s.Classroom)
                        .Where(s => s.CourseId == courseId)
                        .ToListAsync(cancellationToken);

                    var courseSchedule = new CourseScheduleDTO
                    {
                        CourseId = course.Id,
                        CourseNameFr = course.TitleFr,
                        CourseNameAr = course.TitleAr,
                        Description = course.Description,
                        // No grade info available
                        GradeId = Guid.Empty,
                        GradeNameFr = string.Empty,
                        GradeNameAr = string.Empty,
                        Schedule = scheduleItems.Select(s => new ScheduleItemDTO
                        {
                            Id = s.Id,
                            DayOfWeek = s.DayOfWeek,
                            Duration = s.Duration,
                            StartTime = s.StartTime,
                            EndTime = s.EndTime,
                            ClassroomId = s.ClassroomId,
                            ClassroomName = s.Classroom?.RoomTitleFr
                        }).ToList()
                    };
                    result.Add(courseSchedule);
                }
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new List<CourseScheduleDTO>();
            }
        }

        private IQueryable<Teacher> ApplySorting(IQueryable<Teacher> query, string sortBy, bool isAscending)
        {
            // Normalize property name for case-insensitive comparison
            sortBy = sortBy.ToLower();

            if (isAscending)
            {
                return sortBy switch
                {
                    "firstname" or "firstnamefr" => query.OrderBy(t => t.FirstNameFr),
                    "lastname" or "lastnamefr" => query.OrderBy(t => t.LastNameFr),
                    "email" => query.OrderBy(t => t.Email),
                    "hiredate" => query.OrderBy(t => t.HireDate),
                    "specialization" or "specializationfr" => query.OrderBy(t => t.SpecializationFr),
                    "status" => query.OrderBy(t => t.Status),
                    _ => query.OrderBy(t => t.LastNameFr)
                };
            }
            else
            {
                return sortBy switch
                {
                    "firstname" or "firstnamefr" => query.OrderByDescending(t => t.FirstNameFr),
                    "lastname" or "lastnamefr" => query.OrderByDescending(t => t.LastNameFr),
                    "email" => query.OrderByDescending(t => t.Email),
                    "hiredate" => query.OrderByDescending(t => t.HireDate),
                    "specialization" or "specializationfr" => query.OrderByDescending(t => t.SpecializationFr),
                    "status" => query.OrderByDescending(t => t.Status),
                    _ => query.OrderByDescending(t => t.LastNameFr)
                };
            }
        }
    }
}
