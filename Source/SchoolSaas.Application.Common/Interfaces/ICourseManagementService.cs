using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Common.DataObjects.Course;
using SchoolSaas.Domain.Common.DataObjects.Grade;
using SchoolSaas.Domain.Common.DataObjects.Teacher;

namespace SchoolSaas.Application.Common.Interfaces
{
    public interface ICourseManagementService
    {
        Task<bool> CreateCourseAsync(CourseDTO dto, CancellationToken cancellationToken);
        Task<bool> UpdateCourseAsync(Guid courseId, CourseDTO dto, CancellationToken cancellationToken);
        Task<CourseDTO> GetCourseByIdAsync(Guid courseId, CancellationToken cancellationToken); // ✅
        Task<bool> DeleteCourseAsync(Guid courseId, CancellationToken cancellationToken); // ✅
        Task<PagedResult<CourseDTO>> GetPaginatedCoursesAsync(CourseFilterDTO filter, CancellationToken cancellationToken);
        Task<List<CourseDTO>> GetCoursesByGradeLevelAsync(Guid gradeLevelId, CancellationToken cancellationToken);
        Task<PagedResult<CourseDTO>> GetCoursesAssignedToTeacherAsync(Guid teacherId, int page, int? size, CancellationToken cancellationToken); // ✅
        Task<CourseDetailDTO> GetCourseDetailsAsync(Guid courseId, CancellationToken cancellationToken);
        Task<bool> AssignTeacherToCourseAsync(Guid teacherId, Guid courseId, Guid academicYearId, CancellationToken cancellationToken);
        Task<bool> BulkAssignTeachersToCoursesAsync(BulkTeacherAssignmentDTO dto, CancellationToken cancellationToken);
        Task<bool> UnassignTeacherFromCourseAsync(Guid teacherId, Guid courseId, Guid academicYearId, CancellationToken cancellationToken); // ✅

        Task<bool> MapCourseToGradeLevelAsync(CourseGradeMappingDTO dto, CancellationToken cancellationToken);
        Task<List<CourseGradeMappingDTO>> GetCourseMappingsAsync(Guid courseId, CancellationToken cancellationToken); // ✅
        Task<bool> UnmapCourseFromGradeLevelAsync(Guid courseId, Guid gradeLevelId, CancellationToken cancellationToken); // ✅

        Task<bool> BulkUnassignTeachersFromCoursesAsync(BulkTeacherAssignmentDTO dto, CancellationToken cancellationToken);
        Task<List<CourseDTO>> SearchCoursesAsync(string term, CancellationToken cancellationToken);

    }

}
