using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Grade;

namespace SchoolSaas.Application.Backoffice.Courses.Commands.MapCourseToGrade
{
    public class MapCourseToGradeCommand : IRequest<bool>
    {
        public CourseGradeMappingDTO DTO { get; set; }
    }

    public class MapCourseToGradeCommandHandler : IRequestHandler<MapCourseToGradeCommand, bool>
    {
        private readonly ICourseManagementService _service;

        public MapCourseToGradeCommandHandler(ICourseManagementService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(MapCourseToGradeCommand request, CancellationToken cancellationToken)
        {
            return await _service.MapCourseToGradeLevelAsync(request.DTO, cancellationToken);
        }
    }
}