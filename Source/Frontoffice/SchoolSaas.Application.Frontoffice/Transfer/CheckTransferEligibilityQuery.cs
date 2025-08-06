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
    public class CheckTransferEligibilityQuery : IRequest<TransferEligibilityResultDTO>
    {
        public Guid StudentId { get; set; }
    }
    public class CheckTransferEligibilityQueryHandler : IRequestHandler<CheckTransferEligibilityQuery, TransferEligibilityResultDTO>
    {
        private readonly IBackofficeConnectedService _service;
        public CheckTransferEligibilityQueryHandler(IBackofficeConnectedService service) => _service = service;
        public async Task<TransferEligibilityResultDTO> Handle(CheckTransferEligibilityQuery request, CancellationToken ct) =>
            (await _service.CheckTransferEligibility(request.StudentId, ct)).Data;
    }
}
