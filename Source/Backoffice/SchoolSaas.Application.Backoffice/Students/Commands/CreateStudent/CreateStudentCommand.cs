using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Common.DataObjects.Student;

namespace SchoolSaas.Application.Backoffice.Students.Commands.CreateStudent
{
    public class CreateStudentCommand : IRequest<CreateStudentUserDTO>
    {
        public CreateStudentDTO DTO { get; set; }
    }

    public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, CreateStudentUserDTO>
    {
        private readonly IStudentService _service;

        public CreateStudentCommandHandler(IStudentService service)
        {
            _service = service;
        }

        public async Task<CreateStudentUserDTO> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
        {
            return await _service.CreateStudentAsync(request.DTO , cancellationToken);
        }
    }

}