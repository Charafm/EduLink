using MediatR;
using SchoolSaas.Application.Common.Interfaces;

namespace SchoolSaas.Application.Backoffice.Courses.Commands.UnmapCourseFromGrade
{
    public class UnmapCourseToGradeCommand : IRequest<bool>
    {
        public Guid CourseId { get; set; }
        public Guid GradeLevelId { get; set; }
    }

    public class UnmapCourseToGradeCommandHandler : IRequestHandler<UnmapCourseToGradeCommand, bool>
    {
        private readonly ICourseManagementService _service;

        public UnmapCourseToGradeCommandHandler(ICourseManagementService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(UnmapCourseToGradeCommand request, CancellationToken cancellationToken)
        {
            return await _service.UnmapCourseFromGradeLevelAsync(request.CourseId, request.GradeLevelId, cancellationToken);
        }
    }
}