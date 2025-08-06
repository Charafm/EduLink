using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Edulink;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Application.Backoffice.Referential.Cities.Queries
{
    public class SearchCitiesQuery : IRequest<List<CityDTO>>
    {
        public string NameFragment { get; set; }
    }

    public class SearchCitiesQueryHandler : IRequestHandler<SearchCitiesQuery, List<CityDTO>>
    {
        private readonly IEdulinkConnectedService _service;

        public SearchCitiesQueryHandler(IEdulinkConnectedService service)
        {
            _service = service;
        }

        public async Task<List<CityDTO>> Handle(SearchCitiesQuery request, CancellationToken cancellationToken)
        {
            return  _service.SearchCities(request.NameFragment).Result.Data;
        }
    }

}
