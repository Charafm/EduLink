using SchoolSaas.Domain.Common.DataObjects.Enrollment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Application.Common.Interfaces
{
    public interface IEnrollmentPortalService
    {
        Task<bool> SubmitEnrollmentAsync(EnrollmentDTO dto, CancellationToken cancellationToken);
        Task<EnrollmentDetailDTO> GetEnrollmentAsync(Guid enrollmentId, CancellationToken cancellationToken);
        Task<bool> UploadEnrollmentDocumentAsync(EnrollmentDocumentUploadDTO dto, CancellationToken cancellationToken);
        Task<EnrollmentTranscriptDTO> GetEnrollmentTranscriptAsync(Guid enrollmentId, CancellationToken cancellationToken);

    }
}
