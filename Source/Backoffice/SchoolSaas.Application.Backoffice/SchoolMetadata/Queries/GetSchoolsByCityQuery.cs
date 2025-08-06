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
    public class GetSchoolsByCityQuery : IRequest<List<SchoolMetadataDTO>>
    {
        public Guid CityId { get; set; }
    }
    public class GetSchoolsByCityQueryHandler
    : IRequestHandler<GetSchoolsByCityQuery, List<SchoolMetadataDTO>>
    {
        private readonly IEdulinkConnectedService _service;

        public GetSchoolsByCityQueryHandler(IEdulinkConnectedService service)
        {
            _service = service;
        }

        public async Task<List<SchoolMetadataDTO>> Handle(
            GetSchoolsByCityQuery request,
            CancellationToken cancellationToken)
        {
            return  _service.GetSchoolsByCity(
                request.CityId).Result.Data;
        }
    }
}
