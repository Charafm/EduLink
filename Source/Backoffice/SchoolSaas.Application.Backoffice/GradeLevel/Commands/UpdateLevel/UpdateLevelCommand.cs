using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.GradeLevel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Application.Backoffice.GradeLevel.Commands.UpdateLevel
{
    public class UpdateLevelCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public GradeLevelDTO DTO { get; set; }
    }

    public class UpdateLevelCommandHandler : IRequestHandler<UpdateLevelCommand, bool>
    {
        private readonly IGradeLevelService _service;

        public UpdateLevelCommandHandler(IGradeLevelService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(UpdateLevelCommand request, CancellationToken cancellationToken)
        {
            return await _service.UpdateLevel(request.Id, request.DTO, cancellationToken);
        }
    }
}
