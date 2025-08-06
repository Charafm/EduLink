using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SchoolSaas.Application.Backoffice.Frontoffice.TransferPortalService
{
    public class CancelTransferRequestCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
    public class CancelTransferRequestCommandHandler : IRequestHandler<CancelTransferRequestCommand, bool>
    {
        private readonly ITransferPortalService _service;

        public CancelTransferRequestCommandHandler(ITransferPortalService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(CancelTransferRequestCommand request, CancellationToken cancellationToken)
        {
            return await _service.CancelTransferRequestAsync(request.Id, cancellationToken);
        }
    }

}
