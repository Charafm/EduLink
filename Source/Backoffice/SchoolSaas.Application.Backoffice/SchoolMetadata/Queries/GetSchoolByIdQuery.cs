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
    public class GetSchoolByIdQuery : IRequest<SchoolMetadataDTO>
    {
        public Guid SchoolId { get; set; }
    }

    public class GetSchoolByIdQueryHandler : IRequestHandler<GetSchoolByIdQuery, SchoolMetadataDTO>
    {
        private readonly IEdulinkConnectedService _service;

        public GetSchoolByIdQueryHandler(IEdulinkConnectedService service)
        {
            _service = service;
        }

        public async Task<SchoolMetadataDTO> Handle(GetSchoolByIdQuery request, CancellationToken cancellationToken)
        {
            return  _service.GetSchoolById(request.SchoolId).Result.Data;
        }
    }

}
