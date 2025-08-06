using MediatR;
using SchoolSaas.Application.Common.Interfaces;

namespace SchoolSaas.Application.Backoffice.Transfer.Commands.CancelTransferRequest
{
    public class CancelTransferRequestCommand : IRequest<bool>
    {
        public Guid RequestId { get; set; }
    }

    public class CancelTransferRequestCommandHandler : IRequestHandler<CancelTransferRequestCommand, bool>
    {
        private readonly ITransferService _service;

        public CancelTransferRequestCommandHandler(ITransferService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(CancelTransferRequestCommand request, CancellationToken cancellationToken)
        {
            return await _service.CancelTransferRequestAsync(request.RequestId, cancellationToken);
        }
    }
}
