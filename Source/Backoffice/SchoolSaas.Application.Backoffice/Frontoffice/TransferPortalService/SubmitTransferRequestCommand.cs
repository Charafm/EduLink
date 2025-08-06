using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Transfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Application.Backoffice.Frontoffice.TransferPortalService
{
    public class SubmitTransferRequestCommand : IRequest<bool>
    {
        public TransferRequestDTO DATA { get; set; }
    }
    public class SubmitTransferRequestCommandHandler : IRequestHandler<SubmitTransferRequestCommand, bool>
 {
     private readonly ITransferPortalService _service;

     public SubmitTransferRequestCommandHandler(ITransferPortalService service)
     {
         _service = service;
     }

     public async Task<bool> Handle(SubmitTransferRequestCommand request, CancellationToken cancellationToken)
     {
            return await _service.SubmitTransferRequestAsync(request.DATA, cancellationToken);
     }
 }

}
