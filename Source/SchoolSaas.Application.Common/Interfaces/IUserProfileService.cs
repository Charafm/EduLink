using SchoolSaas.Domain.Common.DataObjects.Academic;
using SchoolSaas.Domain.Common.DataObjects.Attendance;
using SchoolSaas.Domain.Common.DataObjects.Common;
using SchoolSaas.Domain.Common.DataObjects.Course;
using SchoolSaas.Domain.Common.DataObjects.Parent;
using SchoolSaas.Domain.Common.DataObjects.Schedule;
using SchoolSaas.Domain.Common.DataObjects.Staff;
using SchoolSaas.Domain.Common.DataObjects.Student;
using SchoolSaas.Domain.Common.DataObjects.Teacher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Application.Common.Interfaces
{
    public interface IUserProfileService
    {
       

        // Student
        Task<StudentDashboardDTO> GetStudentDashboardAsync(Guid studentId, CancellationToken cancellationToken);
        Task<StudentProfileDTO> GetStudentProfile(string userId, CancellationToken cancellationToken);

        // Parent
        Task<ParentDashboardDTO> GetParentDashboardAsync(Guid parentId, CancellationToken cancellationToken);
        Task<ParentProfileDTO> GetParentProfile(string userId, CancellationToken cancellationToken);

        // Teacher
        Task<TeacherDashboardDTO> GetTeacherDashboardAsync(Guid teacherId, CancellationToken cancellationToken);
        Task<TeacherProfileDTO> GetTeacherProfile(string userId, CancellationToken cancellationToken);

        // Staff
        Task<StaffDashboardDTO> GetStaffDashboardAsync(Guid staffId, CancellationToken cancellationToken);
        Task<StaffProfileDTO> GetStaffProfile(string userId, CancellationToken cancellationToken);
    }

}
