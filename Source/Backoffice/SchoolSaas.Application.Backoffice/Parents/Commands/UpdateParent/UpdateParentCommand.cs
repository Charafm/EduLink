using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Parent;

namespace SchoolSaas.Application.Backoffice.Parents.Commands.UpdateParent
{
    public class UpdateParentCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public ParentDTO DTO { get; set; }
    }

    public class UpdateParentCommandHandler : IRequestHandler<UpdateParentCommand, bool>
    {
        private readonly IParentService _service;

        public UpdateParentCommandHandler(IParentService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(UpdateParentCommand request, CancellationToken cancellationToken)
        {
            return await _service.UpdateParentAsync(request.Id, request.DTO, cancellationToken);
        }
    }
}