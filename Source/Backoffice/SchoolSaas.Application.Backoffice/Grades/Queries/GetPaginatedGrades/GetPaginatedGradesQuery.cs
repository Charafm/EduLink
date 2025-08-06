using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Common.DataObjects.Grade;

namespace SchoolSaas.Application.Backoffice.Grades.Queries.GetPaginatedGrades
{
    public class GetPaginatedGradesQuery : IRequest<PagedResult<GradeDTO>>
    {
        public GradeFilterDTO DTO { get; set; }
    }

    public class GetPaginatedGradesQueryHandler : IRequestHandler<GetPaginatedGradesQuery, PagedResult<GradeDTO>>
    {
        private readonly IGradebookService _service;

        public GetPaginatedGradesQueryHandler(IGradebookService service)
        {
            _service = service;
        }

        public async Task<PagedResult<GradeDTO>> Handle(GetPaginatedGradesQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetPaginatedGradesAsync(request.DTO,cancellationToken);
        }
    }
}