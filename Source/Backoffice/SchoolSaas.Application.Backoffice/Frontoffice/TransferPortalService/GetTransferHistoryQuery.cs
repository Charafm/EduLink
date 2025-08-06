using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Common.DataObjects.Transfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Application.Backoffice.Frontoffice.TransferPortalService
{
   
    public class GetTransferHistoryQuery : IRequest<PagedResult<TransferRequestHistoryDTO>>
    {
        public TransferHistoryFilterDTO DATA { get; set; }
    }

    public class GetTransferHistoryQueryHandler : IRequestHandler<GetTransferHistoryQuery, PagedResult<TransferRequestHistoryDTO>>
    {
        private readonly ITransferPortalService _service;

        public GetTransferHistoryQueryHandler(ITransferPortalService service)
        {
            _service = service;
        }

        public async Task<PagedResult<TransferRequestHistoryDTO>> Handle(GetTransferHistoryQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetTransferHistoryAsync(request.DATA , cancellationToken);
        }
    }
}
