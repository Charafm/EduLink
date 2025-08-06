using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Common.DataObjects.Transfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Application.Frontoffice.Transfer
{
    public class GetTransferHistoryQuery : IRequest<ResponseDto<PagedResult<TransferRequestHistoryDTO>>>
    {
        public TransferHistoryFilterDTO Filter { get; set; }
    }

    public class GetTransferHistoryQueryHandler : IRequestHandler<GetTransferHistoryQuery, ResponseDto<PagedResult<TransferRequestHistoryDTO>>>
    {
        private readonly IBackofficeConnectedService _service;
        public GetTransferHistoryQueryHandler(IBackofficeConnectedService service) => _service = service;

        public async Task<ResponseDto<PagedResult<TransferRequestHistoryDTO>>> Handle(GetTransferHistoryQuery request, CancellationToken cancellationToken)
            => await _service.GetTransferHistory(request.Filter, cancellationToken);
    }
}
