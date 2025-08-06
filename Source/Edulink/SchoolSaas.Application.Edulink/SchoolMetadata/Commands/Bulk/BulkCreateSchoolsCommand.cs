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
    public class BulkCreateSchoolsCommand : IRequest<List<SchoolMetadataDTO>>
    {
        public IEnumerable<CreateSchoolMetadataDTO> Schools { get; set; }
    }

    public class BulkCreateSchoolsCommandHandler : IRequestHandler<BulkCreateSchoolsCommand, List<SchoolMetadataDTO>>
    {
        private readonly ISchoolService _service;

        public BulkCreateSchoolsCommandHandler(ISchoolService service)
        {
            _service = service;
        }

        public async Task<List<SchoolMetadataDTO>> Handle(BulkCreateSchoolsCommand request, CancellationToken cancellationToken)
        {
            return await _service.BulkCreateSchoolsAsync(request.Schools, cancellationToken);
        }
    }

}
