using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Common.DataObjects.GradeLevel;
using SchoolSaas.Domain.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Application.Backoffice.GradeLevel.Queries.GetLevelByEducationalStage
{
    public class GetLevelByEducationalStageQuery : IRequest<PagedResult<GradeLevelDTO>>
    {
        public EducationalStageEnum Stage {  get; set; }
        public int page { get; set; }
        public int? size { get; set; }
    }

    public class GetLevelByEducationalStageQueryHandler : IRequestHandler<GetLevelByEducationalStageQuery, PagedResult<GradeLevelDTO>>
    {
        private readonly IGradeLevelService _service;

        public GetLevelByEducationalStageQueryHandler(IGradeLevelService service)
        {
            _service = service;
        }

        public async Task<PagedResult<GradeLevelDTO>> Handle(GetLevelByEducationalStageQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetLevelByEducationalStage(request.Stage,request.page, request.size, cancellationToken);
        }
    }
}
