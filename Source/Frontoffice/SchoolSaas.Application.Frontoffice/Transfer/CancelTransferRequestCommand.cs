using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Application.Frontoffice.Transfer
{
    public class CancelTransferRequestCommand : IRequest<bool>
    {
        public Guid RequestId { get; set; }
    }
    public class CancelTransferRequestCommandHandler : IRequestHandler<CancelTransferRequestCommand, bool>
    {
        private readonly IBackofficeConnectedService _service;
        public CancelTransferRequestCommandHandler(IBackofficeConnectedService service) => _service = service;
        public async Task<bool> Handle(CancelTransferRequestCommand request, CancellationToken ct) =>
            (await _service.CancelTransferRequest(request.RequestId, ct)).Data;
    }

}
