using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Common.DataObjects.Transfer;

namespace SchoolSaas.Application.Backoffice.Transfer.Queries.GetTransferHistory
{
    public class GetTransferHistoryQuery : IRequest<PagedResult<TransferRequestHistoryDTO>>
    {
        public TransferHistoryFilterDTO DTO { get; set; }
    }

    public class GetTransferHistoryQueryHandler : IRequestHandler<GetTransferHistoryQuery, PagedResult<TransferRequestHistoryDTO>>
    {
        private readonly ITransferService _service;

        public GetTransferHistoryQueryHandler(ITransferService service)
        {
            _service = service;
        }

        public async Task<PagedResult<TransferRequestHistoryDTO>> Handle(GetTransferHistoryQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetTransferHistoryAsync(request.DTO, cancellationToken);
        }
    } 
}