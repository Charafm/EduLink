using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Edulink;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Application.Edulink.Referential.Cities.Queries
{
    public class GetCitiesByRegionQuery : IRequest<List<CityDTO>>
    {
        public Guid RegionId { get; set; }
    }

    public class GetCitiesByRegionQueryHandler : IRequestHandler<GetCitiesByRegionQuery, List<CityDTO>>
    {
        private readonly IReferentialService _service;

        public GetCitiesByRegionQueryHandler(IReferentialService service)
        {
            _service = service;
        }

        public async Task<List<CityDTO>> Handle(GetCitiesByRegionQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetCitiesByRegionAsync(request.RegionId, cancellationToken);
        }
    }

}
