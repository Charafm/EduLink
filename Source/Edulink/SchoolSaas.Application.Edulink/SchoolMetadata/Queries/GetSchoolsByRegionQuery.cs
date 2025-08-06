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
    public class GetSchoolsByRegionQuery : IRequest<List<SchoolMetadataDTO>>
    {
        public Guid RegionId { get; set; }
    }
    public class GetSchoolsByRegionQueryHandler
    : IRequestHandler<GetSchoolsByRegionQuery, List<SchoolMetadataDTO>>
    {
        private readonly ISchoolService _service;

        public GetSchoolsByRegionQueryHandler(ISchoolService service)
        {
            _service = service;
        }

        public async Task<List<SchoolMetadataDTO>> Handle(
            GetSchoolsByRegionQuery request,
            CancellationToken cancellationToken)
        {
            return await _service.GetSchoolsByRegionAsync(
                request.RegionId,
                cancellationToken);
        }
    }
}
