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
    public class GetSchoolsByRegionQuery : IRequest<List<SchoolMetadataDTO>>
    {
        public Guid RegionId { get; set; }
    }
    public class GetSchoolsByRegionQueryHandler
    : IRequestHandler<GetSchoolsByRegionQuery, List<SchoolMetadataDTO>>
    {
        private readonly IEdulinkConnectedService _service;

        public GetSchoolsByRegionQueryHandler(IEdulinkConnectedService service)
        {
            _service = service;
        }

        public async Task<List<SchoolMetadataDTO>> Handle(
            GetSchoolsByRegionQuery request,
            CancellationToken cancellationToken)
        {
            return  _service.GetSchoolsByRegion(
                request.RegionId).Result.Data;
        }
    }
}
