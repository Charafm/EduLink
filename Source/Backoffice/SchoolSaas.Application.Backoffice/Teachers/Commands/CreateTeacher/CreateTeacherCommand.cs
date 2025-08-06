using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Common.DataObjects.Teacher;

namespace SchoolSaas.Application.Backoffice.Teachers.Commands.CreateTeacher
{
    public class CreateTeacherCommand : IRequest<CreateUserRequestDto>
    {
        public CreateTeacherDTO DTO { get; set; }
    }

    public class CreateTeacherCommandHandler : IRequestHandler<CreateTeacherCommand, CreateUserRequestDto>
    {
        private readonly ITeacherService _service;

        public CreateTeacherCommandHandler(ITeacherService service)
        {
            _service = service;
        }

        public async Task<CreateUserRequestDto> Handle(CreateTeacherCommand request, CancellationToken cancellationToken)
        {
            return await _service.CreateTeacherAsync(request.DTO, cancellationToken);   
        }
    }
}