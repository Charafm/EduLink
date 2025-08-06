using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Edulink;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Application.Edulink.Referential.Regions.Queries
{
    public class GetRegionByIdQuery : IRequest<RegionDTO>
    {
        public Guid RegionId { get; set; }
    }

    public class GetRegionByIdQueryHandler : IRequestHandler<GetRegionByIdQuery, RegionDTO>
    {
        private readonly IReferentialService _service;

        public GetRegionByIdQueryHandler(IReferentialService service)
        {
            _service = service;
        }

        public async Task<RegionDTO> Handle(GetRegionByIdQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetRegionByIdAsync(request.RegionId, cancellationToken);
        }
    }

}
