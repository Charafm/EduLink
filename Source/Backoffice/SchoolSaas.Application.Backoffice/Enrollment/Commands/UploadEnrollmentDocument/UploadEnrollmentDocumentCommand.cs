using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Enrollment;

namespace SchoolSaas.Application.Backoffice.Enrollment.Commands.UploadEnrollmentDocument
{
    public class UploadEnrollmentDocumentCommand : IRequest<bool>
    {
        public EnrollmentDocumentUploadDTO DTO { get; set; }
    }

    public class UploadEnrollmentDocumentCommandHandler : IRequestHandler<UploadEnrollmentDocumentCommand, bool>
    {
        private readonly IEnrollmentService _service;

        public UploadEnrollmentDocumentCommandHandler(IEnrollmentService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(UploadEnrollmentDocumentCommand request, CancellationToken cancellationToken)
        {
            return await _service.UploadEnrollmentDocumentAsync(request.DTO,cancellationToken);
        }
    }


}