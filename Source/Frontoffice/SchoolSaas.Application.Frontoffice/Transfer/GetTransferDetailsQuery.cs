using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Transfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Application.Frontoffice.Transfer
{
    public class GetTransferDetailsQuery : IRequest<TransferRequestDetailDTO>
    {
        public Guid RequestId { get; set; }
    }
    public class GetTransferDetailsQueryHandler : IRequestHandler<GetTransferDetailsQuery, TransferRequestDetailDTO>
    {
        private readonly IBackofficeConnectedService _service;
        public GetTransferDetailsQueryHandler(IBackofficeConnectedService service) => _service = service;
        public async Task<TransferRequestDetailDTO> Handle(GetTransferDetailsQuery request, CancellationToken ct) =>
            (await _service.GetTransferDetails(request.RequestId, ct)).Data;
    }
}
