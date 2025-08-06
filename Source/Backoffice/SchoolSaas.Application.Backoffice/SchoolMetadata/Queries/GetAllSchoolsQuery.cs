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
    public class GetAllSchoolsQuery : IRequest<List<SchoolMetadataDTO>> { }

    public class GetAllSchoolsQueryHandler : IRequestHandler<GetAllSchoolsQuery, List<SchoolMetadataDTO>>
    {
        private readonly IEdulinkConnectedService _service;

        public GetAllSchoolsQueryHandler(IEdulinkConnectedService service)
        {
            _service = service;
        }

        public async Task<List<SchoolMetadataDTO>> Handle(GetAllSchoolsQuery request, CancellationToken cancellationToken)
        {
            return  _service.GetAllSchools().Result.Data;
        }
    }

}
