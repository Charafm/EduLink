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
    public class GetAllCitiesQuery : IRequest<List<CityDTO>> { }

    public class GetAllCitiesQueryHandler : IRequestHandler<GetAllCitiesQuery, List<CityDTO>>
    {
        private readonly IReferentialService _service;

        public GetAllCitiesQueryHandler(IReferentialService service)
        {
            _service = service;
        }

        public async Task<List<CityDTO>> Handle(GetAllCitiesQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetAllCitiesAsync(cancellationToken);
        }
    }

}
