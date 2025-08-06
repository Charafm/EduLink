using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Course;

namespace SchoolSaas.Application.Backoffice.Courses.Queries.GetCourseById
{
    public class GetCourseByIdQuery : IRequest<CourseDTO>
    {
        public Guid Id { get; set; }
    }

    public class GetCourseByIdQueryHandler : IRequestHandler<GetCourseByIdQuery, CourseDTO>
    {
        private readonly ICourseManagementService _service;

        public GetCourseByIdQueryHandler(ICourseManagementService service)
        {
            _service = service;
        }

        public async Task<CourseDTO> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetCourseByIdAsync(request.Id, cancellationToken);
        }
    }   
}