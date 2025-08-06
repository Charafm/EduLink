using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Course;

namespace SchoolSaas.Application.Backoffice.Courses.Queries.GetCoursesByGrade
{
    public class GetCoursesByGradeQuery : IRequest<List<CourseDTO>>
    {
        public Guid Id { get; set; }
    }

    public class GetCoursesByGradeQueryHandler : IRequestHandler<GetCoursesByGradeQuery, List<CourseDTO>>
    {
        private readonly ICourseManagementService _service;

        public GetCoursesByGradeQueryHandler(ICourseManagementService service)
        {
            _service = service;
        }

        public async Task<List<CourseDTO>> Handle(GetCoursesByGradeQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetCoursesByGradeLevelAsync(request.Id,cancellationToken);
        }
    }
}