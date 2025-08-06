using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Teacher;

namespace SchoolSaas.Application.Backoffice.Courses.Commands.BulkAssignTeachers
{
    public class BulkAssignTeachersCommand : IRequest<bool>
    {
        public BulkTeacherAssignmentDTO DTO { get; set; }
    }

    public class BulkAssignTeachersCommandHandler : IRequestHandler<BulkAssignTeachersCommand, bool>
    {
        private readonly ICourseManagementService _service;

        public BulkAssignTeachersCommandHandler(ICourseManagementService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(BulkAssignTeachersCommand request, CancellationToken cancellationToken)
        {
            return await _service.BulkAssignTeachersToCoursesAsync(request.DTO, cancellationToken);
        }
    }
}