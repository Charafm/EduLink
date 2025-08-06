using MediatR;
using SchoolSaas.Application.Common.Interfaces;

namespace SchoolSaas.Application.Backoffice.Teachers.Commands.ReassignTeacherToCourse
{
    public class ReassignTeacherToCourseCommand : IRequest<bool>
    {
        public Guid TeacherId { get; set; }
        public Guid CourseId { get; set; }
    }
    public class ReassignTeacherToCourseHandler : IRequestHandler<ReassignTeacherToCourseCommand, bool>
    {
        private readonly ITeacherService _teacherService;

        public ReassignTeacherToCourseHandler(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        public async Task<bool> Handle(ReassignTeacherToCourseCommand request, CancellationToken cancellationToken)
        {
            return await _teacherService.ReassignTeacherToCourseAsync(request.TeacherId, request.CourseId, cancellationToken);
        }
    }

}
