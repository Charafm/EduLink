using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Transfer;

namespace SchoolSaas.Application.Backoffice.Transfer.Commands.UpdateTransferStatus
{
    public class UpdateTransferStatusCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public TransferStatusUpdateDTO DTO { get; set; }
    }

    public class UpdateTransferStatusCommandHandler : IRequestHandler<UpdateTransferStatusCommand, bool>
    {
        private readonly ITransferService _service;

        public UpdateTransferStatusCommandHandler(ITransferService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(UpdateTransferStatusCommand request, CancellationToken cancellationToken)
        {
            return await _service.UpdateTransferStatusAsync(request.Id, request.DTO,cancellationToken);
        }
    }

}
