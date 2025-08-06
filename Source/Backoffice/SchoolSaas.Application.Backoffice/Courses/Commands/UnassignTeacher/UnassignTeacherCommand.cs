using MediatR;
using SchoolSaas.Application.Common.Interfaces;

namespace SchoolSaas.Application.Backoffice.Courses.Commands.UnassignTeacher
{
    public class UnassignTeacherCommand : IRequest<bool>
    {
        public Guid TeacherId { get; set; }
        public Guid CourseId { get; set; }
        public Guid AcademicYearId { get; set; }
    }

    public class UnassignTeacherCommandHandler : IRequestHandler<UnassignTeacherCommand, bool>
    {
        private readonly ICourseManagementService _service;

        public UnassignTeacherCommandHandler(ICourseManagementService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(UnassignTeacherCommand request, CancellationToken cancellationToken)
        {
            return await _service.UnassignTeacherFromCourseAsync(request.TeacherId, request.CourseId, request.AcademicYearId, cancellationToken);
        }
    }
}