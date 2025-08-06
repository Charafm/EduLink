using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Enrollment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Infrastructure.Backoffice.Services.FrontofficeServices
{
    public  class EnrollmentPortalService : IEnrollmentPortalService
    {
        private readonly IEnrollmentService _service;

        public EnrollmentPortalService(IEnrollmentService service)
        {
            _service = service;
        }

        public async Task<bool> SubmitEnrollmentAsync(EnrollmentDTO dto, CancellationToken cancellationToken)
        {
            return await _service.SubmitEnrollmentAsync(dto,   cancellationToken);
        }
        public async Task<EnrollmentDetailDTO> GetEnrollmentAsync(Guid enrollmentId, CancellationToken cancellationToken)
        {
            return await _service.GetEnrollmentAsync(enrollmentId, cancellationToken);
        }
        public async Task<bool> UploadEnrollmentDocumentAsync(EnrollmentDocumentUploadDTO dto, CancellationToken cancellationToken)
        {
            return await _service.UploadEnrollmentDocumentAsync(dto, cancellationToken);
        }
        public async Task<EnrollmentTranscriptDTO> GetEnrollmentTranscriptAsync(Guid enrollmentId, CancellationToken cancellationToken)
        {
            return await _service.GetEnrollmentTranscriptAsync(enrollmentId, cancellationToken);
        }
    }
}
