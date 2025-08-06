using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Transfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Application.Backoffice.Transfer.Commands.UpdateTransferRequest
{
    public class UpdateTransferRequestCommand : IRequest<bool>
    {
        public Guid TransferId { get; set; }
        public TransferRequestUpdateDTO TransferRequest { get; set; }
    }

    public class UpdateTransferRequestCommandHandler : IRequestHandler<UpdateTransferRequestCommand, bool>
    {
        private readonly ITransferService _service;

        public UpdateTransferRequestCommandHandler(ITransferService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(UpdateTransferRequestCommand request, CancellationToken cancellationToken)
        {
            return await _service.UpdateTransferRequestAsync(request.TransferId, request.TransferRequest, cancellationToken);
        }
    }

}
