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
    public class CreateSchoolCommand : IRequest<SchoolMetadataDTO>
    {
        public CreateSchoolMetadataDTO School { get; set; }
    }

    public class CreateSchoolCommandHandler : IRequestHandler<CreateSchoolCommand, SchoolMetadataDTO>
    {
        private readonly ISchoolService _service;

        public CreateSchoolCommandHandler(ISchoolService service)
        {
            _service = service;
        }

        public async Task<SchoolMetadataDTO> Handle(CreateSchoolCommand request, CancellationToken cancellationToken)
        {
            return await _service.CreateSchoolAsync(request.School, cancellationToken);
        }
    }

}
