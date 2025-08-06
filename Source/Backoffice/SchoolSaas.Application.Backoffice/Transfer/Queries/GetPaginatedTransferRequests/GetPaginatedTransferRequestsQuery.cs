using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Common.DataObjects.Transfer;

namespace SchoolSaas.Application.Backoffice.Transfer.Queries.GetPaginatedTransferRequests
{
    public class GetPaginatedTransferRequestsQuery : IRequest<PagedResult<TransferRequestDTO>>
    {
        public TransferRequestFilterDTO DTO { get; set; }
    }

    public class GetPaginatedTransferRequestsQueryHandler : IRequestHandler<GetPaginatedTransferRequestsQuery, PagedResult<TransferRequestDTO>>
    {
        private readonly ITransferService _service;

        public GetPaginatedTransferRequestsQueryHandler(ITransferService service)
        {
            _service = service;
        }

        public async Task<PagedResult<TransferRequestDTO>> Handle(GetPaginatedTransferRequestsQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetPaginatedTransferRequestsAsync(request.DTO, cancellationToken);
        }
    }
}
