using MediatR;
using SchoolSaas.Application.Common.Interfaces;

namespace SchoolSaas.Application.Backoffice.Enrollment.Commands.MoveEnrollmentToNextStep
{
    public class MoveEnrollmentToNextStepCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }

    public class MoveEnrollmentToNextStepCommandHandler : IRequestHandler<MoveEnrollmentToNextStepCommand, bool>
    {
        private readonly IEnrollmentService _service;

        public MoveEnrollmentToNextStepCommandHandler(IEnrollmentService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(MoveEnrollmentToNextStepCommand request, CancellationToken cancellationToken)
        {
            return await _service.MoveEnrollmentToNextStepAsync(request.Id, cancellationToken);
        }
    }


}