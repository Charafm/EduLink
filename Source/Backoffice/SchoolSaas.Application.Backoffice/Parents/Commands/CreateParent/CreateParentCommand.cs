using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Parent;

namespace SchoolSaas.Application.Backoffice.Parents.Commands.CreateParent
{
    public class CreateParentCommand : IRequest<bool>
    {
        public ParentDTO DTO { get; set; }
    }

    public class CreateParentCommandHandler : IRequestHandler<CreateParentCommand, bool>
    {
        private readonly IParentService _service;

        public CreateParentCommandHandler(IParentService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(CreateParentCommand request, CancellationToken cancellationToken)
        {
            return await _service.CreateParentAsync(request.DTO, cancellationToken);
        }
    }
}