using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Teacher;

namespace SchoolSaas.Application.Backoffice.Teachers.Commands.UpdateTeacher
{
    public class UpdateTeacherCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public TeacherDTO DTO { get; set; }
    }

    public class UpdateTeacherCommandHandler : IRequestHandler<UpdateTeacherCommand, bool>
    {
        private readonly ITeacherService _service;

        public UpdateTeacherCommandHandler(ITeacherService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(UpdateTeacherCommand request, CancellationToken cancellationToken)
        {
            return await _service.UpdateTeacherAsync(request.Id, request.DTO, cancellationToken);
        }
    }
}