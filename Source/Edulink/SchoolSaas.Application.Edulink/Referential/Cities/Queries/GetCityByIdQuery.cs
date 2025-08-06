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
    public class GetCityByIdQuery : IRequest<CityDTO>
    {
        public Guid CityId { get; set; }
    }

    public class GetCityByIdQueryHandler : IRequestHandler<GetCityByIdQuery, CityDTO>
    {
        private readonly IReferentialService _service;

        public GetCityByIdQueryHandler(IReferentialService service)
        {
            _service = service;
        }

        public async Task<CityDTO> Handle(GetCityByIdQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetCityByIdAsync(request.CityId, cancellationToken);
        }
    }

}
