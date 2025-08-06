using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Common.DataObjects.Common;
using SchoolSaas.Domain.Common.DataObjects.Enrollment;

namespace SchoolSaas.Application.Common.Interfaces
{
    public interface IEnrollmentService
    {
        Task<bool> SubmitEnrollmentAsync(EnrollmentDTO dto, CancellationToken cancellationToken);
        Task<bool> UpdateEnrollmentStatusAsync(Guid enrollmentId, EnrollmentStatusUpdateDTO dto, CancellationToken cancellationToken);
        Task<EnrollmentDetailDTO> GetEnrollmentAsync(Guid enrollmentId, CancellationToken cancellationToken);
        Task<PagedResult<EnrollmentDTO>> GetPaginatedEnrollmentsAsync(EnrollmentFilterDTO filter, CancellationToken cancellationToken);
        Task<bool> BulkUpdateEnrollmentStatusAsync(BulkEnrollmentStatusUpdateDTO dto, CancellationToken cancellationToken);
        Task<bool> MoveEnrollmentToNextStepAsync(Guid enrollmentId, CancellationToken cancellationToken);
        Task<EnrollmentMetricsDTO> GetEnrollmentDashboardMetricsAsync(DateRangeDTO dateRange, CancellationToken cancellationToken);
        Task<PagedResult<EnrollmentStatusHistoryDTO>> GetEnrollmentHistoryAsync(EnrollmentHistoryFilterDTO filter, CancellationToken cancellationToken);
        Task<bool> UploadEnrollmentDocumentAsync(EnrollmentDocumentUploadDTO dto, CancellationToken cancellationToken);
        //Task<bool> BulkUploadEnrollmentDocumentsAsync(BulkEnrollmentDocumentsDTO dto, CancellationToken cancellationToken);
        Task<bool> VerifyEnrollmentDocumentAsync(Guid documentId, EnrollmentDocumentVerificationDTO dto, CancellationToken cancellationToken);
        Task<EnrollmentTranscriptDTO> GetEnrollmentTranscriptAsync(Guid enrollmentId, CancellationToken cancellationToken);
        // For managing Enrollment Requests

        
            // ----- REQUESTS -----
            Task<Guid> CreateEnrollmentRequestAsync(CreateEnrollmentRequestDTO dto, CancellationToken ct);
            Task<PagedResult<EnrollmentRequestDTO>> GetEnrollmentRequestsAsync(EnrollmentRequestFilterDTO filter, CancellationToken ct);
            Task<EnrollmentRequestDetailDTO> GetEnrollmentRequestByIdAsync(Guid requestId, CancellationToken ct);
            Task<bool> UpdateEnrollmentRequestAsync(Guid requestId, UpdateEnrollmentRequestDTO dto, CancellationToken ct);
         Task<bool> SubmitEnrollmentRequestAsync(Guid requestId, CancellationToken ct);

            // approval: generates real Enrollment and transitions request
         Task<bool> ApproveEnrollmentRequestAsync(Guid requestId, Guid adminUserId, CancellationToken ct);
         Task<bool> BulkApproveEnrollmentRequestsAsync(IEnumerable<Guid> requestIds, Guid adminUserId, CancellationToken ct);

         Task <bool> SaveRequest (EnrollmentRequestDTO request, CancellationToken ct);
        


    }
}
