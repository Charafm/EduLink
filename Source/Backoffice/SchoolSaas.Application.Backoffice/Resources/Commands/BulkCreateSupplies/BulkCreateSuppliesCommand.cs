using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.SchoolSupply;

namespace SchoolSaas.Application.Backoffice.Resources.Commands.BulkCreateSupplies
{
    public class BulkCreateSuppliesCommand : IRequest<bool>
    {
        public BulkSchoolSupplyDTO DTO { get; set; }
    }

    public class BulkCreateSuppliesCommandHandler : IRequestHandler<BulkCreateSuppliesCommand, bool>
    {
        private readonly ISchoolSupplyService _service;

        public BulkCreateSuppliesCommandHandler(ISchoolSupplyService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(BulkCreateSuppliesCommand request, CancellationToken cancellationToken)
        {
            return await _service.BulkCreateSuppliesAsync(request.DTO, cancellationToken);
        }
    }

}