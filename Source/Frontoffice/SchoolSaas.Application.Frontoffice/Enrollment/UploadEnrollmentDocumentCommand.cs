using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Enrollment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Application.Frontoffice.Enrollment
{
    public class UploadEnrollmentDocumentCommand : IRequest<bool>
    {
        public EnrollmentDocumentUploadDTO Dto { get; set; }
    }
    public class UploadEnrollmentDocumentCommandHandler : IRequestHandler<UploadEnrollmentDocumentCommand, bool>
    {
        private readonly IBackofficeConnectedService _service;
        public UploadEnrollmentDocumentCommandHandler(IBackofficeConnectedService service) => _service = service;
        public async Task<bool> Handle(UploadEnrollmentDocumentCommand request, CancellationToken ct) =>
            (await _service.UploadEnrollmentDocument(request.Dto, ct)).Data;
    }

}
