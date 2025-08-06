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
    public class BulkDeleteSchoolsCommand : IRequest<bool>
    {
        public IEnumerable<Guid> SchoolIds { get; set; }
    }

    public class BulkDeleteSchoolsCommandHandler : IRequestHandler<BulkDeleteSchoolsCommand, bool>
    {
        private readonly ISchoolService _service;

        public BulkDeleteSchoolsCommandHandler(ISchoolService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(BulkDeleteSchoolsCommand request, CancellationToken cancellationToken)
        {
            return await _service.BulkDeleteSchoolsAsync(request.SchoolIds, cancellationToken);
        }
    }
}
