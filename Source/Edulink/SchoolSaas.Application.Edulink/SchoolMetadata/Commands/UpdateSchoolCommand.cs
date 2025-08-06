using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Edulink;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Application.Edulink.SchoolMetadata.Commands
{
    public class UpdateSchoolCommand : IRequest<SchoolMetadataDTO>
    {
        public Guid SchoolId { get; set; }
        public UpdateSchoolMetadataDTO School { get; set; }
    }

    public class UpdateSchoolCommandHandler : IRequestHandler<UpdateSchoolCommand, SchoolMetadataDTO>
    {
        private readonly ISchoolService _service;

        public UpdateSchoolCommandHandler(ISchoolService service)
        {
            _service = service;
        }

        public async Task<SchoolMetadataDTO> Handle(UpdateSchoolCommand request, CancellationToken cancellationToken)
        {
            return await _service.UpdateSchoolAsync(request.SchoolId, request.School, cancellationToken);
        }
    }

}
