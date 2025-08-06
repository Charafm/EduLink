using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Transfer;

namespace SchoolSaas.Application.Backoffice.Transfer.Queries.GetTransferRequestDetails
{
    public class GetTransferRequestDetailsQuery : IRequest<TransferRequestDetailDTO>
    {
        public Guid RequestId { get; set; }
    }
    public class GetTransferRequestDetailsQueryHandler : IRequestHandler<GetTransferRequestDetailsQuery, TransferRequestDetailDTO>
    {
        private readonly ITransferService _service;

        public GetTransferRequestDetailsQueryHandler(ITransferService service)
        {
            _service = service;
        }

        public async Task<TransferRequestDetailDTO> Handle(GetTransferRequestDetailsQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetTransferRequestDetailsAsync(request.RequestId, cancellationToken);
        }
    }
}
