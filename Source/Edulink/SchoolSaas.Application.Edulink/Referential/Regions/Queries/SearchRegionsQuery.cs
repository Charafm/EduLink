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
    public class SearchRegionsQuery : IRequest<List<RegionDTO>>
    {
        public string NameFragment { get; set; }
    }

    public class SearchRegionsQueryHandler : IRequestHandler<SearchRegionsQuery, List<RegionDTO>>
    {
        private readonly IReferentialService _service;

        public SearchRegionsQueryHandler(IReferentialService service)
        {
            _service = service;
        }

        public async Task<List<RegionDTO>> Handle(SearchRegionsQuery request, CancellationToken cancellationToken)
        {
            return await _service.SearchRegionsAsync(request.NameFragment, cancellationToken);
        }
    }

}
