using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Transfer;

namespace SchoolSaas.Application.Backoffice.Transfer.Queries.CheckTransferEligibility
{
    public class CheckTransferEligibilityQuery : IRequest<TransferEligibilityResultDTO>
    {
        public Guid Id { get; set; }
    }

    public class CheckTransferEligibilityQueryHandler : IRequestHandler<CheckTransferEligibilityQuery, TransferEligibilityResultDTO>
    {
        private readonly ITransferService _service;

        public CheckTransferEligibilityQueryHandler(ITransferService service)
        {
            _service = service;
        }

        public async Task<TransferEligibilityResultDTO> Handle(CheckTransferEligibilityQuery request, CancellationToken cancellationToken)
        {
            return await _service.CheckTransferEligibilityAsync(request.Id, cancellationToken);
        }
    }
}
