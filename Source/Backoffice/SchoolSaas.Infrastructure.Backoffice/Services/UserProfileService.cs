using Microsoft.EntityFrameworkCore;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Backoffice.Academic;
using SchoolSaas.Domain.Backoffice.Students;
using SchoolSaas.Domain.Common.DataObjects.Common;
using SchoolSaas.Domain.Common.DataObjects.Course;
using SchoolSaas.Domain.Common.DataObjects.Notification;
using SchoolSaas.Domain.Common.DataObjects.Parent;
using SchoolSaas.Domain.Common.DataObjects.Schedule;
using SchoolSaas.Domain.Common.DataObjects.Staff;
using SchoolSaas.Domain.Common.DataObjects.Student;
using SchoolSaas.Domain.Common.DataObjects.Teacher;
using SchoolSaas.Domain.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Infrastructure.Backoffice.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IBackofficeContext _dbContext;
        private readonly IBackofficeReadOnlyContext _dbReadOnlyContext;
        private readonly IServiceHelper _serviceHelper;
        

        public UserProfileService(
            IBackofficeContext dbContext,
            IBackofficeReadOnlyContext dbReadOnlyContext,
            IServiceHelper serviceHelper)
        {
            _dbContext = dbContext;
            _dbReadOnlyContext = dbReadOnlyContext;
            _serviceHelper = serviceHelper;
        }
        public async Task<StudentDashboardDTO> GetStudentDashboardAsync(Guid studentId, CancellationToken cancellationToken)
        {
            var student = await _dbReadOnlyContext.Students
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == studentId, cancellationToken)
                ?? throw new KeyNotFoundException("Student not found.");

            var grades = await _dbReadOnlyContext.Grades
                .AsNoTracking()
                .Where(g => g.StudentId == studentId)
                .ToListAsync(cancellationToken);
            double gpa = grades.Any() ? grades.Average(g => g.Score) : 0;

            var attendance = await _dbReadOnlyContext.Attendances
                .AsNoTracking()
                .Where(a => a.StudentId == studentId)
                .ToListAsync(cancellationToken);
            int totalAbsences = attendance.Count(a => a.Status == AttendanceEnum.Absent);
            int totalLate = attendance.Count(a => a.Status == AttendanceEnum.Late);

            var mappings = await _dbReadOnlyContext.GradeSectionStudentMappings
                .AsNoTracking()
                .Where(m => m.StudentId == studentId)
                .Include(m => m.GradeSection)
                    .ThenInclude(gs => gs.GradeLevel)
                        .ThenInclude(gl => gl.CourseGradeMappings)
                            .ThenInclude(cgm => cgm.Course)
                                .ThenInclude(c => c.TeacherCourseAssignments)
                                    .ThenInclude(tca => tca.Teacher)
                .ToListAsync(cancellationToken);

            var dashboardCourses = mappings
                .SelectMany<GradeSectionStudentMapping, CourseGradeMapping>(m => (IEnumerable<CourseGradeMapping>)m.GradeSection.GradeLevel.CourseGradeMappings)
                .Select(cgm => cgm.Course)
                .GroupBy(c => c.Id)
                .Select(g => g.First())
                .Select(course =>
                {
                    var teacher = course.TeacherCourseAssignments.Teacher;

                    return new DashboardCourseDTO
                    {
                        CourseId = course.Id,
                        CourseNameFr = course.TitleFr,
                        CourseNameAr = course.TitleAr,
                        TeacherNameFr = teacher == null
                            ? "—"
                            : $"{teacher.FirstNameFr} {teacher.LastNameFr}",
                        TeacherNameAr = teacher == null
                            ? "—"
                            : $"{teacher.FirstNameAr} {teacher.LastNameAr}",

                        Schedule = "TBD"
                    };
                })
                .ToList();

            return new StudentDashboardDTO
            {
                StudentId = student.Id,
                FullNameFR = $"{student.FirstNameFr} {student.LastNameFr}",
                FullNameAR = $"{student.FirstNameAr} {student.LastNameAr}",
                GPA = gpa,
                TotalAbsences = totalAbsences,
                TotalLate = totalLate,
                Courses = dashboardCourses
            };
        }
        public async Task<StudentProfileDTO> GetStudentProfile(string userId, CancellationToken cancellationToken)
        {
            var student = await _dbReadOnlyContext.Students
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.UserId == userId, cancellationToken)
                ?? throw new KeyNotFoundException("Student not found.");
            var gradeSection = await _dbReadOnlyContext.GradeSectionStudentMappings
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.StudentId == student.Id, cancellationToken)
                ?? throw new KeyNotFoundException("Student not found.");


            return new StudentProfileDTO
            {
                StudentId = student.Id,
                FullNameFr = $"{student.FirstNameFr} {student.LastNameFr}",
                FullNameAr = $"{student.FirstNameAr} {student.LastNameAr}",
                DateOfBirth = student.DateOfBirth,
                GradeSectionFr = gradeSection.GradeSection.SectionNameFr,
                GrafeSectionAr = gradeSection.GradeSection.SectionNameAr,
                GradeLevelFr = gradeSection.GradeSection.GradeLevel.TitleFr, 
                GradeLevelAr= gradeSection.GradeSection.GradeLevel.TitleAr, 
                Email = student.Detail.Email,
                Phone = student.Detail.Phone,
                Status = student.Status
            };
        }

        public async Task<ParentDashboardDTO> GetParentDashboardAsync(Guid parentId, CancellationToken cancellationToken)
        {
            var parentUserId = await _dbReadOnlyContext.Parents.Where(p => p.Id == parentId).Select(p => p.UserId).FirstOrDefaultAsync();
            // Notifications
            var notifications = await _dbReadOnlyContext.Notifications
                .AsNoTracking()
                .Where(n => n.UserId == parentUserId)
                .OrderByDescending(n => n.Created)
                .Take(10)
                .Select(n => new NotificationDTO
                {
                    NotificationId = n.Id,
                    Title = n.NotificationBody.Title,
                    Message = n.NotificationBody.Description,
                    IsRead = n.IsRead,
                    CreatedAt = n.Created
                })
                .ToListAsync(cancellationToken);

            // Associated students & statuses
            var students = await _dbReadOnlyContext.StudentParents
                .AsNoTracking()
                .Where(sp => sp.ParentId == parentId)
                .Include(sp => sp.Student)
                .Select(sp => new AssociatedStudentStatusDTO
                {
                    StudentId = sp.StudentId,
                    FullName = sp.Student.FirstNameFr + " " + sp.Student.LastNameFr,
                    Status = sp.Student.Status
                })
                .ToListAsync(cancellationToken);

            // Recent activities (parent audits)
            var activities = await _dbReadOnlyContext.ParentAudits
                .AsNoTracking()
                .Where(a => a.ParentId == parentId)
                .OrderByDescending(a => a.ActionDate)
                .Take(10)
                .Select(a => new RecentActivityDTO
                {
                    ActionDate = a.ActionDate,
                    ActionType = a.ActionType.ToString(),
                    Details = a.Details
                })
                .ToListAsync(cancellationToken);

            return new ParentDashboardDTO
            {
                ParentId = parentId,
                Notifications = notifications,
                Students = students,
                RecentActivities = activities
            };
        }
        public async Task<ParentProfileDTO> GetParentProfile(string userId, CancellationToken cancellationToken)
        {
            var parent = await _dbReadOnlyContext.Parents
               .AsNoTracking()
               .FirstOrDefaultAsync(s => s.UserId == userId, cancellationToken)
               ?? throw new KeyNotFoundException("Student not found.");
           
            var associatedStudents= await _dbReadOnlyContext.StudentParents
               .AsNoTracking()
               .Where(s => s.ParentId == parent.Id).ToListAsync(cancellationToken)
               ?? throw new KeyNotFoundException("Student not found.");

            return new ParentProfileDTO
            {
                ParentId = parent.Id,
                FullNameFr = $"{parent.FirstNameFr} {parent.LastNameFr}",
                FullNameAr = $"{parent.FirstNameAr} {parent.LastNameAr}",
                Occupation = parent.Occupation,
                AssociatedStudentsCount = associatedStudents.Count,
                Children = await MapToDTO(associatedStudents),
                Email = parent.Email,
                Phone = parent.Phone,
                
            };
        }
        private async Task<List<StudentDTO>> MapToDTO (List<StudentParent> student)
        {
            var result = new List<StudentDTO>();
            foreach (var studentParent in student)
            {
                result.Append(new StudentDTO
                {
                    Id = studentParent.StudentId,
                    UserId = studentParent.Student.UserId,
                  
                    FirstNameAr = studentParent.Student.FirstNameAr,
                    FirstNameFr = studentParent.Student.FirstNameFr,
                    LastNameAr = studentParent.Student.LastNameAr,
                    LastNameFr = studentParent.Student.LastNameFr,
                    DateOfBirth = studentParent.Student.DateOfBirth,
                    Gender = studentParent.Student.Gender,
            
                    Status = studentParent.Student.Status,

                });
            }
            return result;
        }
        public async Task<TeacherProfileDTO> GetTeacherProfile(string userId, CancellationToken cancellationToken)
        {
            var teacher = await _dbReadOnlyContext.Teachers
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.UserId == userId, cancellationToken);

            if (teacher == null)
                throw new KeyNotFoundException("Teacher not found.");

            var courses =  GetTeacherAssignedCoursesAsync(teacher.Id, cancellationToken);

            return new TeacherProfileDTO
            {
                TeacherId = teacher.Id,
                FullNameFr = $"{teacher.FirstNameFr} {teacher.LastNameFr}",
                FullNameAr = $"{teacher.FirstNameAr} {teacher.LastNameAr}",
                Email = teacher.Email,
                Phone = teacher.Phone,
                HireDate = teacher.HireDate,
                SpecializationFr = teacher.SpecializationFr,
                SpecializationAr = teacher.SpecializationAr,
                Status = teacher.Status,
                Courses = courses.Result,
            };
        }
        public async Task<StaffDashboardDTO> GetStaffDashboardAsync(Guid staffId, CancellationToken cancellationToken)
        {
            var staff = await _dbReadOnlyContext.Staffs
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == staffId, cancellationToken)
                ?? throw new KeyNotFoundException("Staff member not found.");

            var auditLogs = await _dbReadOnlyContext.StaffAudits
                .AsNoTracking()
                .Where(a => a.StaffId == staffId)
                .OrderByDescending(a => a.ActionDate)
                .Take(10)
                .Select(a => new StaffAuditDTO
                {
                    ActionDate = a.ActionDate,
                    ActionType = a.ActionType,
                    Details = a.Details,
                    PerformedBy = a.PerformedBy
                })
                .ToListAsync(cancellationToken);

            return new StaffDashboardDTO
            {
                StaffId = staff.Id,
                FullName = $"{staff.FirstNameFr} {staff.LastNameFr}",
                JobTitle = staff.JobTitleFr,
                Department = staff.DepartmentFr,
                NotificationsCount = 0, // Optional: if you have Staff-level notifications
                RecentActivity = auditLogs
            };
        }

        public async Task<TeacherDashboardDTO> GetTeacherDashboardAsync(Guid teacherId, CancellationToken cancellationToken)
        {
            var teacher = await _dbReadOnlyContext.Teachers
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == teacherId, cancellationToken)
                ?? throw new KeyNotFoundException("Teacher not found.");

            var courseAssignments = await _dbReadOnlyContext.TeacherCourseAssignments
                .AsNoTracking()
                .Where(tca => tca.TeacherId == teacherId)
                .Include(tca => tca.Course)
                .ToListAsync(cancellationToken);

            var courseSummaries = courseAssignments
                .Select(tca => new CourseDTO
                {

                    TitleFr = tca.Course.TitleFr,
                    TitleAr = tca.Course.TitleAr
                })
                .ToList();

            return new TeacherDashboardDTO
            {
                TeacherId = teacher.Id,
                FullNameFr = $"{teacher.FirstNameFr} {teacher.LastNameFr}",
                FullNameAr = $"{teacher.FirstNameAr} {teacher.LastNameAr}",
                CourseCount = courseSummaries.Count,
                AssignedCourses = courseSummaries
            };
        }

        private async Task<List<CourseScheduleDTO>> GetTeacherAssignedCoursesAsync(Guid teacherId, CancellationToken cancellationToken)
        {
            try
            {
                // Retrieve course IDs assigned to this teacher via TeacherCourseAssignment
                var courseIds =  _dbReadOnlyContext.TeacherCourseAssignments
                    .AsNoTracking()
                    .Where(tc => tc.TeacherId == teacherId) // !tc.IsDeleted
                    .Select(tc => tc.CourseId)
                    .ToListAsync(cancellationToken).Result;

                if (!courseIds.Any())
                    return new List<CourseScheduleDTO>();

                var result = new List<CourseScheduleDTO>();

                foreach (var courseId in courseIds)
                {
                    // Retrieve course details
                    var course =  _dbReadOnlyContext.Courses
                        .AsNoTracking()
                        //.Include(c => c.Grade) // Removed because Course does not include a Grade navigation property
                        .FirstOrDefaultAsync(c => c.Id == courseId, cancellationToken).Result;
                    if (course == null)
                        continue;

                    // Retrieve schedule items for this course
                    var scheduleItems =  _dbReadOnlyContext.Schedules
                        .AsNoTracking()
                        .Include(s => s.Classroom)
                        .Where(s => s.CourseId == courseId)
                        .ToListAsync(cancellationToken).Result;

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
        public async Task<StaffProfileDTO> GetStaffProfile(string userId, CancellationToken cancellationToken)
        {
            // Retrieve the staff record.
            var staff = await _dbReadOnlyContext.Staffs
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.UserId == userId && !(s.IsDeleted.HasValue), cancellationToken)
                ?? throw new KeyNotFoundException("Staff record not found.");

            // Retrieve audit logs for this staff member.
            var auditLogs = await _dbReadOnlyContext.StaffAudits
                .AsNoTracking()
                .Where(a => a.StaffId == staff.Id)
                .OrderByDescending(a => a.ActionDate)
                .Select(a => new StaffAuditDTO
                {
                    ActionDate = a.ActionDate,
                    ActionType = a.ActionType,
                    Details = a.Details,
                    PerformedBy = a.PerformedBy
                })
                .ToListAsync(cancellationToken);

            return new StaffProfileDTO
            {
                StaffId = staff.Id,
                FullName = $"{staff.FirstNameFr} {staff.LastNameFr}",
                Email = staff.Email,
                Phone = staff.Phone,
                Department = staff.DepartmentFr, // Adjust if you prefer Arabic or combine
                JobTitle = staff.JobTitleFr,       // Adjust if needed
                Role = staff.Role,
                AuditLogs = auditLogs
            };
        }
    }
}
