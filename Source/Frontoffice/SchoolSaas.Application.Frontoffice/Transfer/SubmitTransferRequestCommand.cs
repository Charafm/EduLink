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
    public class SubmitTransferRequestCommand : IRequest<bool>
    {
        public TransferRequestDTO Dto { get; set; }
    }
    public class SubmitTransferRequestCommandHandler : IRequestHandler<SubmitTransferRequestCommand, bool>
    {
        private readonly IBackofficeConnectedService _service;
        public SubmitTransferRequestCommandHandler(IBackofficeConnectedService service) => _service = service;
        public async Task<bool> Handle(SubmitTransferRequestCommand request, CancellationToken ct) =>
            (await _service.SubmitTransferRequest(request.Dto, ct)).Data;
    }

}
