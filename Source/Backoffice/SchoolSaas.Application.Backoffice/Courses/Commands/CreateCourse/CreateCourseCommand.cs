using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Course;

namespace SchoolSaas.Application.Backoffice.Courses.Commands.CreateCourse
{
    public class CreateCourseCommand : IRequest<bool>
    {
        public CourseDTO DTO { get; set; }
    }

    public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, bool>
    {
        private readonly ICourseManagementService _service;

        public CreateCourseCommandHandler(ICourseManagementService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            return await _service.CreateCourseAsync(request.DTO, cancellationToken);
        }
    }
}