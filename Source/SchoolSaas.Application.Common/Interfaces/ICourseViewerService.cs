using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Common.DataObjects.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Application.Common.Interfaces
{
    public interface ICourseViewerService
    {
        Task<CourseDetailDTO> GetCourseDetailsAsync(Guid courseId, CancellationToken cancellationToken);
        Task<List<CourseDTO>> GetCoursesByGradeLevelAsync(Guid gradeLevelId, CancellationToken cancellationToken);
        Task<PagedResult<CourseDTO>> GetCoursesAssignedToTeacherAsync(Guid teacherId, int page, int? size, CancellationToken cancellationToken);

    }
}
