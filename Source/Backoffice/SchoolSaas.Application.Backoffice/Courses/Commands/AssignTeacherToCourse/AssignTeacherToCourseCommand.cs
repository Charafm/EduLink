using MediatR;
using SchoolSaas.Application.Common.Interfaces;

namespace SchoolSaas.Application.Backoffice.Courses.Commands.AssignTeacherToCourse
{
    public class AssignTeacherToCourseCommand : IRequest<bool>
    {
        public Guid TeacherId { get; set; }
        public Guid CourseId { get; set; }
        public Guid AcademicYearId  { get; set; }
    }

    public class AssignTeacherToCourseCommandHandler : IRequestHandler<AssignTeacherToCourseCommand, bool>
    {
        private readonly ICourseManagementService _service;

        public AssignTeacherToCourseCommandHandler(ICourseManagementService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(AssignTeacherToCourseCommand request, CancellationToken cancellationToken)
        {
            return await _service.AssignTeacherToCourseAsync(request.TeacherId, request.CourseId, request.AcademicYearId, cancellationToken);
        }
    }
}