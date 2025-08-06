using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Common.DataObjects.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Infrastructure.Backoffice.Services.FrontofficeServices
{
    public class CourseViewerService : ICourseViewerService
    {
        private readonly ICourseManagementService _service;

        public CourseViewerService(ICourseManagementService service)
        {
            _service = service;
        }
        public async Task<CourseDetailDTO> GetCourseDetailsAsync(Guid courseId, CancellationToken cancellationToken)
        {
            return await _service.GetCourseDetailsAsync(courseId, cancellationToken);
        }
        public async Task<List<CourseDTO>> GetCoursesByGradeLevelAsync(Guid gradeLevelId, CancellationToken cancellationToken)
        {
            return await _service.GetCoursesByGradeLevelAsync(gradeLevelId, cancellationToken);
        }
        public async Task<PagedResult<CourseDTO>> GetCoursesAssignedToTeacherAsync(Guid teacherId, int page, int? size, CancellationToken cancellationToken)
        {
            return await _service.GetCoursesAssignedToTeacherAsync(teacherId, page, size, cancellationToken);
        }
    }
}
