using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Enrollment;

namespace SchoolSaas.Application.Backoffice.Enrollment.Commands.VerifyEnrollmentDocument
{
    public class VerifyEnrollmentDocumentCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public EnrollmentDocumentVerificationDTO DTO { get; set; }
    }

    public class VerifyEnrollmentDocumentCommandHandler : IRequestHandler<VerifyEnrollmentDocumentCommand, bool>
    {
        private readonly IEnrollmentService _service;

        public VerifyEnrollmentDocumentCommandHandler(IEnrollmentService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(VerifyEnrollmentDocumentCommand request, CancellationToken cancellationToken)
        {
            return await _service.VerifyEnrollmentDocumentAsync(request.Id,request.DTO, cancellationToken);
        }
    }


}