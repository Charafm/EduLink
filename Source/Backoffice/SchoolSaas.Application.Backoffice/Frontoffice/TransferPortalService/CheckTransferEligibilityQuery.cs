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
  
    public class CheckTransferEligibilityQuery : IRequest<TransferEligibilityResultDTO>
    {
            public Guid Id  { get; set; }
    }

    public class CheckTransferEligibilityQueryHandler : IRequestHandler<CheckTransferEligibilityQuery, TransferEligibilityResultDTO>
    {
        private readonly ITransferPortalService _service;

        public CheckTransferEligibilityQueryHandler(ITransferPortalService service)
        {
            _service = service;
        }

        public async Task<TransferEligibilityResultDTO> Handle(CheckTransferEligibilityQuery request, CancellationToken cancellationToken)
        {
            return await _service.CheckTransferEligibilityAsync(request.Id , cancellationToken);
        }
    }
}
