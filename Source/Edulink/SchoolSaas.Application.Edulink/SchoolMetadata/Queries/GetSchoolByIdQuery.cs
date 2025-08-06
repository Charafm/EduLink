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
    public class GetSchoolByIdQuery : IRequest<SchoolMetadataDTO>
    {
        public Guid SchoolId { get; set; }
    }

    public class GetSchoolByIdQueryHandler : IRequestHandler<GetSchoolByIdQuery, SchoolMetadataDTO>
    {
        private readonly ISchoolService _service;

        public GetSchoolByIdQueryHandler(ISchoolService service)
        {
            _service = service;
        }

        public async Task<SchoolMetadataDTO> Handle(GetSchoolByIdQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetSchoolByIdAsync(request.SchoolId, cancellationToken);
        }
    }

}
