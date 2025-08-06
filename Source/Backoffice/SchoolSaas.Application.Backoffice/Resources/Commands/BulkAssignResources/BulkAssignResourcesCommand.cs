using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.SchoolSupply;

namespace SchoolSaas.Application.Backoffice.Resources.Commands.BulkAssignResources
{
    public class BulkAssignResourcesCommand : IRequest<bool>
    {
        public BulkResourceAssignmentDTO DTO   { get; set; }
    }

    public class BulkAssignResourcesCommandHandler : IRequestHandler<BulkAssignResourcesCommand, bool>
    {
        private readonly ISchoolSupplyService _service;

        public BulkAssignResourcesCommandHandler(ISchoolSupplyService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(BulkAssignResourcesCommand request, CancellationToken cancellationToken)
        {
            return await _service.BulkAssignResourcesAsync(request.DTO, cancellationToken);
        }
    }

}