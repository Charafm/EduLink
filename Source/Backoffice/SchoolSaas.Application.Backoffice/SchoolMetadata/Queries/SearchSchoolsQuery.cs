using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Edulink;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Application.Backoffice.SchoolMetadata.Queries
{
    public class SearchSchoolsQuery : IRequest<List<SchoolMetadataDTO>>
    {
        public string NameFragment { get; set; }
    }
    public class SearchSchoolsQueryHandler
    : IRequestHandler<SearchSchoolsQuery, List<SchoolMetadataDTO>>
    {
        private readonly IEdulinkConnectedService _service;

        public SearchSchoolsQueryHandler(IEdulinkConnectedService service)
        {
            _service = service;
        }

        public async Task<List<SchoolMetadataDTO>> Handle(
            SearchSchoolsQuery request,
            CancellationToken cancellationToken)
        {
            return  _service.SearchSchool(
                request.NameFragment).Result.Data;
        }
    }
}
