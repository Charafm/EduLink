using MediatR;
using SchoolSaas.Application.Common.Interfaces;

namespace SchoolSaas.Application.Backoffice.Teachers.Commands.UnassignTeacherFromCourse
{
    public class UnassignTeacherFromCourseCommand : IRequest<bool>
    {
        public Guid TeacherId { get; set; }
        public Guid CourseId { get; set; }
    }
    public class UnassignTeacherFromCourseHandler : IRequestHandler<UnassignTeacherFromCourseCommand, bool>
    {
        private readonly ITeacherService _teacherService;

        public UnassignTeacherFromCourseHandler(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        public async Task<bool> Handle(UnassignTeacherFromCourseCommand request, CancellationToken cancellationToken)
        {
            return await _teacherService.UnassignTeacherFromCourseAsync(request.TeacherId, request.CourseId, cancellationToken);
        }
    }

}
