using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Enrollment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Application.Backoffice.Frontoffice.EnrollmentPortalService
{
   
    public class UploadEnrollmentDocumentCommand : IRequest<bool>
    {
        public EnrollmentDocumentUploadDTO Data { get; set; }
    }

    public class UploadEnrollmentDocumentCommandHandler : IRequestHandler<UploadEnrollmentDocumentCommand, bool>
    {
        private readonly IEnrollmentPortalService _service;

        public UploadEnrollmentDocumentCommandHandler(IEnrollmentPortalService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(UploadEnrollmentDocumentCommand request, CancellationToken cancellationToken)
        {
            return await _service.UploadEnrollmentDocumentAsync(request.Data, cancellationToken);
        }
    }

}
