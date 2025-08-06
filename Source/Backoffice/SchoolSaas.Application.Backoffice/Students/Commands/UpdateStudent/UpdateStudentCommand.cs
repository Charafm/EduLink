using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Student;

namespace SchoolSaas.Application.Backoffice.Students.Commands.UpdateStudent
{
    public class UpdateStudentCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public StudentDTO DTO { get; set; }
    }

    public class UpdateStudentCommandHandler : IRequestHandler<UpdateStudentCommand, bool>
    {
        private readonly IStudentService _service;

        public UpdateStudentCommandHandler(IStudentService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
        {
            return await _service.UpdateStudentAsync(request.Id, request.DTO, cancellationToken);
        }
    }

}