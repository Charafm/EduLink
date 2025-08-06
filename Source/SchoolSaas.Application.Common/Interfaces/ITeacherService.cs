using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Common.DataObjects.Course;
using SchoolSaas.Domain.Common.DataObjects.Teacher;
using SchoolSaas.Domain.Common.Enums;

namespace SchoolSaas.Application.Common.Interfaces
{
    public interface ITeacherService
    {
        Task<CreateUserRequestDto> CreateTeacherAsync(CreateTeacherDTO dto, CancellationToken cancellationToken);
        Task<TeacherDTO> GetTeacherByUserId(string userId);
        Task<bool> UpdateTeacherAsync(Guid teacherId, TeacherDTO dto, CancellationToken cancellationToken);
        Task<TeacherDTO> GetTeacherByIdAsync(Guid teacherId, CancellationToken cancellationToken);
        Task<PagedResult<TeacherDTO>> GetPaginatedTeachersAsync(FilterTeacherDTO filter, CancellationToken cancellationToken);
        Task<bool> DeleteTeacherAsync(Guid teacherId, CancellationToken cancellationToken);
        Task<bool> UpdateTeacherEmploymentStatusAsync(Guid teacherId, TeacherStatusEnum newStatus, CancellationToken cancellationToken);
        Task<List<CourseScheduleDTO>> GetTeacherAssignedCoursesAsync(Guid teacherId, CancellationToken cancellationToken);
        Task<bool> ReassignTeacherToCourseAsync(Guid teacherId, Guid courseId, CancellationToken cancellationToken);
        Task<bool> UnassignTeacherFromCourseAsync(Guid teacherId, Guid courseId, CancellationToken cancellationToken);
        
    }

}
