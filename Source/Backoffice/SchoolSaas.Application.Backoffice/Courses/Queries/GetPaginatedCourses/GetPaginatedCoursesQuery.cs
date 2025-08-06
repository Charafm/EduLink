using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Common.DataObjects.Course;

namespace SchoolSaas.Application.Backoffice.Courses.Queries.GetPaginatedCourses
{
    public class GetPaginatedCoursesQuery : IRequest<PagedResult<CourseDTO>>
    {
        public CourseFilterDTO DTO { get; set; }
    }

    public class GetPaginatedCoursesQueryHandler : IRequestHandler<GetPaginatedCoursesQuery, PagedResult<CourseDTO>>
    {
        private readonly ICourseManagementService _service;

        public GetPaginatedCoursesQueryHandler(ICourseManagementService service)
        {
            _service = service;
        }

        public async Task<PagedResult<CourseDTO>> Handle(GetPaginatedCoursesQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetPaginatedCoursesAsync(request.DTO,cancellationToken);
        }
    }
}