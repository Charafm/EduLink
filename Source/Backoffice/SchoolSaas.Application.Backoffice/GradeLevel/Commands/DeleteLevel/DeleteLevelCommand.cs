using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Application.Backoffice.GradeLevel.Commands.DeleteLevel
{
    public class DeleteLevelCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }

    public class DeleteLevelCommandHandler : IRequestHandler<DeleteLevelCommand, bool>
    {
        private readonly IGradeLevelService _service;

        public DeleteLevelCommandHandler(IGradeLevelService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(DeleteLevelCommand request, CancellationToken cancellationToken)
        {
            return await _service.DeleteLevel(request.Id, cancellationToken);
        }
    }
}
