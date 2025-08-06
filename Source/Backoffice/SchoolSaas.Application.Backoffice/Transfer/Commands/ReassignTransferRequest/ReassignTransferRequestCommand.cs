using MediatR;
using SchoolSaas.Application.Common.Interfaces;

namespace SchoolSaas.Application.Backoffice.Transfer.Commands.ReassignTransferRequest
{
    public class ReassignTransferRequestCommand : IRequest<bool>
    {
        public Guid RequestId { get; set; }
        public Guid NewTargetBranchId { get; set; }
    }
    public class ReassignTransferRequestCommandHandler : IRequestHandler<ReassignTransferRequestCommand, bool>
    {
        private readonly ITransferService _service;

        public ReassignTransferRequestCommandHandler(ITransferService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(ReassignTransferRequestCommand request, CancellationToken cancellationToken)
        {
            return await _service.ReassignTransferRequestAsync(request.RequestId, request.NewTargetBranchId, cancellationToken);
        }
    }
}
