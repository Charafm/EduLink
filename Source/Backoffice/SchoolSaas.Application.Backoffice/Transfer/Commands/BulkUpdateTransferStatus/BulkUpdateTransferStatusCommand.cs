using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Transfer;

namespace SchoolSaas.Application.Backoffice.Transfer.Commands.BulkUpdateTransferStatus
{
    public class BulkUpdateTransferStatusCommand : IRequest<bool>
    {
        public BulkTransferUpdateDTO DTO { get; set; }
    }

    public class BulkUpdateTransferStatusCommandHandler : IRequestHandler<BulkUpdateTransferStatusCommand, bool>
    {
        private readonly ITransferService _service;

        public BulkUpdateTransferStatusCommandHandler(ITransferService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(BulkUpdateTransferStatusCommand request, CancellationToken cancellationToken)
        {
            return await _service.BulkUpdateTransferStatusAsync(request.DTO,cancellationToken);
        }
    }

}
