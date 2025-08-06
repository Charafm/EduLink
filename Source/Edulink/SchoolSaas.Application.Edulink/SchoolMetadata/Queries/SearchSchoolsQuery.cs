using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Edulink;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Application.Edulink.SchoolMetadata.Queries
{
    public class SearchSchoolsQuery : IRequest<List<SchoolMetadataDTO>>
    {
        public string NameFragment { get; set; }
    }
    public class SearchSchoolsQueryHandler
    : IRequestHandler<SearchSchoolsQuery, List<SchoolMetadataDTO>>
    {
        private readonly ISchoolService _service;

        public SearchSchoolsQueryHandler(ISchoolService service)
        {
            _service = service;
        }

        public async Task<List<SchoolMetadataDTO>> Handle(
            SearchSchoolsQuery request,
            CancellationToken cancellationToken)
        {
            return await _service.SearchSchoolsAsync(
                request.NameFragment,
                cancellationToken);
        }
    }
}
