using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Application.Edulink.SchoolMetadata.Commands
{
    public class DeleteSchoolCommand : IRequest<bool>
    {
        public Guid SchoolId { get; set; }
    }

    public class DeleteSchoolCommandHandler : IRequestHandler<DeleteSchoolCommand, bool>
    {
        private readonly ISchoolService _service;

        public DeleteSchoolCommandHandler(ISchoolService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(DeleteSchoolCommand request, CancellationToken cancellationToken)
        {
            return await _service.DeleteSchoolAsync(request.SchoolId, cancellationToken);
        }
    }

}
