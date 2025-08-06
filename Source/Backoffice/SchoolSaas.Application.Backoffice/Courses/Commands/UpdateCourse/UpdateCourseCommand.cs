using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Course;

namespace SchoolSaas.Application.Backoffice.Courses.Commands.UpdateCourse
{
    public class UpdateCourseCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public CourseDTO DTO { get; set; }
    }

    public class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseCommand, bool>
    {
        private readonly ICourseManagementService _service;

        public UpdateCourseCommandHandler(ICourseManagementService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
        {
            return await _service.UpdateCourseAsync(request.Id, request.DTO,cancellationToken);
        }
    }
}