using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Student;

namespace SchoolSaas.Application.Backoffice.Students.Commands.TransitionStudentStatus
{
    public class TransitionStudentStatusCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public StudentStatusTransitionDTO DTO   { get; set; }
    }

    public class TransitionStudentStatusCommandHandler : IRequestHandler<TransitionStudentStatusCommand, bool>
    {
        private readonly IStudentService _service;

        public TransitionStudentStatusCommandHandler(IStudentService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(TransitionStudentStatusCommand request, CancellationToken cancellationToken)
        {
            return await _service.TransitionStudentStatusAsync(request.Id, request.DTO, cancellationToken);
        }
    }

}