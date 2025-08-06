using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Common.DataObjects.Transfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Application.Common.Interfaces
{
    public interface ITransferPortalService
    {
        Task<bool> SubmitTransferRequestAsync(TransferRequestDTO dto, CancellationToken cancellationToken);
        Task<bool> CancelTransferRequestAsync(Guid requestId, CancellationToken cancellationToken);
        Task<PagedResult<TransferRequestHistoryDTO>> GetTransferHistoryAsync(TransferHistoryFilterDTO filter, CancellationToken cancellationToken);
        Task<TransferEligibilityResultDTO> CheckTransferEligibilityAsync(Guid studentId, CancellationToken cancellationToken);
        Task<TransferRequestDetailDTO> GetTransferRequestDetailsAsync(Guid requestId, CancellationToken cancellationToken);

    }
}
