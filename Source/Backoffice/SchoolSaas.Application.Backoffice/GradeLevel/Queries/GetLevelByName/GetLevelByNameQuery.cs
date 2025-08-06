using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Common.DataObjects.GradeLevel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Application.Backoffice.GradeLevel.Queries.GetLevelByName
{
    public class GetLevelByNameQuery : IRequest<GradeLevelDTO>
    {
        public string Name { get; set; }
    }

    public class GetLevelByNameQueryHandler : IRequestHandler<GetLevelByNameQuery, GradeLevelDTO>
    {
        private readonly IGradeLevelService _service;

        public GetLevelByNameQueryHandler(IGradeLevelService service)
        {
            _service = service;
        }

        public async Task<GradeLevelDTO> Handle(GetLevelByNameQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetLevelByName(request.Name, cancellationToken);
        }
    }
}
