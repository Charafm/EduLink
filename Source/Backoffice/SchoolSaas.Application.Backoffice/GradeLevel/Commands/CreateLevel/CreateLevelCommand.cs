using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.GradeLevel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Application.Backoffice.GradeLevel.Commands.CreateLevel
{
    public class CreateLevelCommand : IRequest<bool>
    {
        public GradeLevelDTO DTO { get; set; }
    }

    public class CreateLevelCommandHandler : IRequestHandler<CreateLevelCommand, bool>
    {
        private readonly IGradeLevelService _service;

        public CreateLevelCommandHandler(IGradeLevelService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(CreateLevelCommand request, CancellationToken cancellationToken)
        {
            return await _service.CreateLevel(request.DTO , cancellationToken); 
        }
    }
}
