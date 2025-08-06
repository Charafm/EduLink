using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Common.DataObjects.Transfer;

namespace SchoolSaas.Application.Common.Interfaces
{
    public interface ITransferService
    {
        Task<bool> SubmitTransferRequestAsync(TransferRequestDTO dto, CancellationToken cancellationToken);
        Task<bool> UpdateTransferStatusAsync(Guid transferRequestId, TransferStatusUpdateDTO dto,
            CancellationToken cancellationToken);
        Task<PagedResult<TransferRequestDTO>> GetPaginatedTransferRequestsAsync(
            TransferRequestFilterDTO filter, CancellationToken cancellationToken);
        Task<bool> BulkUpdateTransferStatusAsync(BulkTransferUpdateDTO dto,
            CancellationToken cancellationToken);
        Task<TransferEligibilityResultDTO> CheckTransferEligibilityAsync(Guid studentId,
            CancellationToken cancellationToken);
        Task<PagedResult<TransferRequestHistoryDTO>> GetTransferHistoryAsync(
            TransferHistoryFilterDTO filter, CancellationToken cancellationToken);
        Task<bool> CancelTransferRequestAsync(Guid requestId, CancellationToken cancellationToken);
       
        Task<bool> ReassignTransferRequestAsync(Guid requestId, Guid newTargetBranchId, CancellationToken cancellationToken);
        Task<TransferRequestDetailDTO> GetTransferRequestDetailsAsync(Guid requestId, CancellationToken cancellationToken);
        Task<bool> UpdateTransferRequestAsync(Guid transferRequestId, TransferRequestUpdateDTO dto, CancellationToken cancellationToken);
    }
}

