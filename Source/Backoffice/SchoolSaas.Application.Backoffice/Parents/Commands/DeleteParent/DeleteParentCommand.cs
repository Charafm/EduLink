using MediatR;
using SchoolSaas.Application.Common.Interfaces;

namespace SchoolSaas.Application.Backoffice.Parents.Commands.DeleteParent
{
    public class DeleteParentCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }

    public class DeleteParentCommandHandler : IRequestHandler<DeleteParentCommand, bool>
    {
        private readonly IParentService _service;

        public DeleteParentCommandHandler(IParentService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(DeleteParentCommand request, CancellationToken cancellationToken)
        {
            return await _service.DeleteParentAsync(request.Id, cancellationToken);
        }
    }
}