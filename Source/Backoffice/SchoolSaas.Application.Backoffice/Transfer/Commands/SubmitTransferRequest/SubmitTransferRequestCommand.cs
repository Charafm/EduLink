using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Transfer;

namespace SchoolSaas.Application.Backoffice.Transfer.Commands.SubmitTransferRequest
{
    public class SubmitTransferRequestCommand : IRequest<bool>
    {
        public TransferRequestDTO DTO { get; set; }
    }

    public class SubmitTransferRequestCommandHandler : IRequestHandler<SubmitTransferRequestCommand, bool>
    {
        private readonly ITransferService _service;

        public SubmitTransferRequestCommandHandler(ITransferService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(SubmitTransferRequestCommand request, CancellationToken cancellationToken)
        {
            return await _service.SubmitTransferRequestAsync(request.DTO, cancellationToken);
        }
    }

}
