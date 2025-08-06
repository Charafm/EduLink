using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Enrollment;

namespace SchoolSaas.Application.Backoffice.Enrollment.Commands.SubmitEnrollment
{
    public class SubmitEnrollmentCommand : IRequest<bool>
    {
        public EnrollmentDTO DTO    { get; set; }
    }

    public class SubmitEnrollmentCommandHandler : IRequestHandler<SubmitEnrollmentCommand, bool>
    {
        private readonly IEnrollmentService _service;

        public SubmitEnrollmentCommandHandler(IEnrollmentService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(SubmitEnrollmentCommand request, CancellationToken cancellationToken)
        {
            return await _service.SubmitEnrollmentAsync(request.DTO, cancellationToken);
        }
    }


}