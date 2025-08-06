using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Common.DataObjects.Course;

namespace SchoolSaas.Application.Backoffice.Courses.Queries.GetCoursesAssignedToTeacher
{
    public class GetCoursesAssignedToTeacherQuery : IRequest<PagedResult<CourseDTO>>
    {
        public Guid Id { get; set; }
        public int page {  get; set; }
        public int? size { get; set; }
    }

    public class GetCoursesAssignedToTeacherQueryHandler : IRequestHandler<GetCoursesAssignedToTeacherQuery, PagedResult<CourseDTO>>
    {
        private readonly ICourseManagementService _service;

        public GetCoursesAssignedToTeacherQueryHandler(ICourseManagementService service)
        {
            _service = service;
        }

        public async Task<PagedResult<CourseDTO>> Handle(GetCoursesAssignedToTeacherQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetCoursesAssignedToTeacherAsync(request.Id, request.page, request.size, cancellationToken);
        }
    }
}