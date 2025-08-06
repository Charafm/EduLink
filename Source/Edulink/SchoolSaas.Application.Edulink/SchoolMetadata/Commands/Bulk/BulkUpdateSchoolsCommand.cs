using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Edulink;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Application.Edulink.SchoolMetadata.Commands.Bulk
{
    public class BulkUpdateSchoolsCommand : IRequest<List<SchoolMetadataDTO>>
    {
        public IEnumerable<UpdateSchoolMetadataDTO> Schools { get; set; }
    }

    public class BulkUpdateSchoolsCommandHandler : IRequestHandler<BulkUpdateSchoolsCommand, List<SchoolMetadataDTO>>
    {
        private readonly ISchoolService _service;

        public BulkUpdateSchoolsCommandHandler(ISchoolService service)
        {
            _service = service;
        }

        public async Task<List<SchoolMetadataDTO>> Handle(BulkUpdateSchoolsCommand request, CancellationToken cancellationToken)
        {
            return await _service.BulkUpdateSchoolsAsync(request.Schools, cancellationToken);
        }
    }
}
