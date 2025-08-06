using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Parent;

namespace SchoolSaas.Application.Backoffice.Parents.Commands.BulkCreateParents
{
    public class BulkCreateParentsCommand : IRequest<bool>
    {
        public BulkParentDTO DTO { get; set; }
    }

    public class BulkCreateParentsCommandHandler : IRequestHandler<BulkCreateParentsCommand, bool>
    {
        private readonly IParentService _service;

        public BulkCreateParentsCommandHandler(IParentService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(BulkCreateParentsCommand request, CancellationToken cancellationToken)
        {
            return await _service.BulkCreateParentsAsync(request.DTO,cancellationToken);
        }
    }
}