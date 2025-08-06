using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Common.DataObjects.Academic;

namespace SchoolSaas.Application.Backoffice.Academics.Queries.GetPaginatedSemesters
{
    public class GetPaginatedSemestersQuery : IRequest<PagedResult<SemesterDTO>>
    {
        public Guid academicYearId { get; set; }
        public FilterSemesterDTO filter {  get; set; }
    }

    public class GetPaginatedSemestersQueryHandler : IRequestHandler<GetPaginatedSemestersQuery, PagedResult<SemesterDTO>>
    {
        private readonly IAcademicsService _service;

        public GetPaginatedSemestersQueryHandler(IAcademicsService service)
        {
            _service = service;
        }

        public async Task<PagedResult<SemesterDTO>> Handle(GetPaginatedSemestersQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetPaginatedSemestersForYearAsync(request.academicYearId, request.filter, cancellationToken);
        }
    }
}