using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Common.DataObjects.GradeLevel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Application.Backoffice.GradeLevel.Queries.GetLevelById
{
    public class GetLevelByIdQuery : IRequest<GradeLevelDTO>
    {
        public Guid Id { get; set; }
    }

    public class GetLevelByIdQueryHandler : IRequestHandler<GetLevelByIdQuery, GradeLevelDTO>
    {
        private readonly IGradeLevelService _service;

        public GetLevelByIdQueryHandler(IGradeLevelService service)
        {
            _service = service;
        }

        public async Task<GradeLevelDTO> Handle(GetLevelByIdQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetLevel(request.Id, cancellationToken);
        }
    }
}
