using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Common.DataObjects.Transfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Infrastructure.Backoffice.Services.FrontofficeServices
{
    public class TransferPortalService : ITransferPortalService
    {
        private readonly ITransferService _transferService;

        public TransferPortalService(ITransferService transferService)
        {
            _transferService = transferService;
        }


        public async Task<bool> SubmitTransferRequestAsync(TransferRequestDTO dto, CancellationToken cancellationToken)
        {
            return await _transferService.SubmitTransferRequestAsync(dto, cancellationToken);
        }
        public async Task<bool> CancelTransferRequestAsync(Guid requestId, CancellationToken cancellationToken)
        {
            return await _transferService.CancelTransferRequestAsync(requestId, cancellationToken);
        }
        public async Task<PagedResult<TransferRequestHistoryDTO>> GetTransferHistoryAsync(TransferHistoryFilterDTO filter, CancellationToken cancellationToken)
        {
            return await _transferService.GetTransferHistoryAsync(filter, cancellationToken);
        }
        public async Task<TransferEligibilityResultDTO> CheckTransferEligibilityAsync(Guid studentId, CancellationToken cancellationToken)
        {
            return await _transferService.CheckTransferEligibilityAsync(studentId, cancellationToken);
        }
        public async Task<TransferRequestDetailDTO> GetTransferRequestDetailsAsync(Guid requestId, CancellationToken cancellationToken)
        {
            return await _transferService.GetTransferRequestDetailsAsync(requestId, cancellationToken);
        }
    }
}
