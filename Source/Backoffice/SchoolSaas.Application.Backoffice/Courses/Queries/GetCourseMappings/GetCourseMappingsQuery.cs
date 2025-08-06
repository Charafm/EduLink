using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Grade;

namespace SchoolSaas.Application.Backoffice.Courses.Queries.GetCourseMappings
{
    public class GetCourseMappingsQuery : IRequest<List<CourseGradeMappingDTO>>
    {
        public Guid Id { get; set; }
    }

    public class GetCourseMappingsQueryHandler : IRequestHandler<GetCourseMappingsQuery, List<CourseGradeMappingDTO>>
    {
        private readonly ICourseManagementService _service;

        public GetCourseMappingsQueryHandler(ICourseManagementService service)
        {
            _service = service;
        }

        public async Task<List<CourseGradeMappingDTO>> Handle(GetCourseMappingsQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetCourseMappingsAsync(request.Id, cancellationToken);
        }
    }
}