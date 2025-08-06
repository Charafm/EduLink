using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Course;

namespace SchoolSaas.Application.Backoffice.Teachers.Queries.GetAssignedCourses
{
    public class GetAssignedCoursesQuery : IRequest<List<CourseScheduleDTO>>
    {
        public Guid Id { get; set; }
    }

    public class GetAssignedCoursesQueryHandler : IRequestHandler<GetAssignedCoursesQuery, List<CourseScheduleDTO>>
    {
        private readonly ITeacherService _service;

        public GetAssignedCoursesQueryHandler(ITeacherService service)
        {
            _service = service;
        }

        public async Task<List<CourseScheduleDTO>> Handle(GetAssignedCoursesQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetTeacherAssignedCoursesAsync(request.Id, cancellationToken);
        }
    }
}