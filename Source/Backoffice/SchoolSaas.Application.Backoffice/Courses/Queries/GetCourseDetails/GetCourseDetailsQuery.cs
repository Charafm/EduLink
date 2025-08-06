using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Course;

namespace SchoolSaas.Application.Backoffice.Courses.Queries.GetCourseDetails
{
    public class GetCourseDetailsQuery : IRequest<CourseDetailDTO>
    {
        public Guid Id { get; set; }
    }

    public class GetCourseDetailsQueryHandler : IRequestHandler<GetCourseDetailsQuery, CourseDetailDTO>
    {
        private readonly ICourseManagementService _service;

        public GetCourseDetailsQueryHandler(ICourseManagementService service)
        {
            _service = service;
        }

        public async Task<CourseDetailDTO> Handle(GetCourseDetailsQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetCourseDetailsAsync(request.Id, cancellationToken);
        }
    }
}
