using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Student;

namespace SchoolSaas.Application.Backoffice.Students.Commands.UpdateStudentDetails
{
    public class UpdateStudentDetailsCommand : IRequest<bool>
    {
        public Guid Id  { get; set; }
        public StudentDetailDTO DTO { get; set; }
    }

    public class UpdateStudentDetailsCommandHandler : IRequestHandler<UpdateStudentDetailsCommand, bool>
    {
        private readonly IStudentService _service;

        public UpdateStudentDetailsCommandHandler(IStudentService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(UpdateStudentDetailsCommand request, CancellationToken cancellationToken)
        {
            return await _service.UpdateStudentDetailsAsync(request.Id, request.DTO, cancellationToken);
        }
    }

}