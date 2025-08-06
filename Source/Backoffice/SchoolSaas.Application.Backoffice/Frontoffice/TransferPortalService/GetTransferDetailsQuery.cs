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
    
    public class GetTransferDetailsQuery : IRequest<TransferRequestDetailDTO>
    {
        public Guid Id { get; set; }
    }

    public class GetTransferDetailsQueryHandler : IRequestHandler<GetTransferDetailsQuery, TransferRequestDetailDTO>
    {
        private readonly ITransferPortalService _service;

        public GetTransferDetailsQueryHandler(ITransferPortalService service)
        {
            _service = service;
        }

        public async Task<TransferRequestDetailDTO> Handle(GetTransferDetailsQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetTransferRequestDetailsAsync(request.Id, cancellationToken);
        }
    }
}
