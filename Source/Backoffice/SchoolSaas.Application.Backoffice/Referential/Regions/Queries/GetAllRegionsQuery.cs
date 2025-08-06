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
    public class GetAllRegionsQuery : IRequest<List<RegionDTO>> { }

    public class GetAllRegionsQueryHandler : IRequestHandler<GetAllRegionsQuery, List<RegionDTO>>
    {
        private readonly IEdulinkConnectedService _service;

        public GetAllRegionsQueryHandler(IEdulinkConnectedService service)
        {
            _service = service;
        }

        public async Task<List<RegionDTO>> Handle(GetAllRegionsQuery request, CancellationToken cancellationToken)
        {
            return  _service.GetAllRegions().Result.Data;
        }
    }

}
