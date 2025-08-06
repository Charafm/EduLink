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
    public class GetAllSchoolsQuery : IRequest<List<SchoolMetadataDTO>> { }

    public class GetAllSchoolsQueryHandler : IRequestHandler<GetAllSchoolsQuery, List<SchoolMetadataDTO>>
    {
        private readonly ISchoolService _service;

        public GetAllSchoolsQueryHandler(ISchoolService service)
        {
            _service = service;
        }

        public async Task<List<SchoolMetadataDTO>> Handle(GetAllSchoolsQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetAllSchoolsAsync(cancellationToken);
        }
    }

}
