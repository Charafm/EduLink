using MediatR;
using SchoolSaas.Application.Common.Interfaces;

namespace SchoolSaas.Application.Backoffice.Courses.Commands.DeleteCourse
{
    public class DeleteCourseCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }

    public class DeleteCourseCommandHandler : IRequestHandler<DeleteCourseCommand, bool>
    {
        private readonly ICourseManagementService _service;

        public DeleteCourseCommandHandler(ICourseManagementService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
        {
            return await _service.DeleteCourseAsync(request.Id, cancellationToken);
        }
    }
}