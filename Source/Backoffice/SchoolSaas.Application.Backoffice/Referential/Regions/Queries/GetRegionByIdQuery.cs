using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Edulink;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Application.Backoffice.Referential.Regions.Queries
{
    public class GetRegionByIdQuery : IRequest<RegionDTO>
    {
        public Guid RegionId { get; set; }
    }

    public class GetRegionByIdQueryHandler : IRequestHandler<GetRegionByIdQuery, RegionDTO>
    {
        private readonly IEdulinkConnectedService _service;

        public GetRegionByIdQueryHandler(IEdulinkConnectedService service)
        {
            _service = service;
        }

        public async Task<RegionDTO> Handle(GetRegionByIdQuery request, CancellationToken cancellationToken)
        {
            return  _service.GetRegionsById(request.RegionId).Result.Data;
        }
    }

}
