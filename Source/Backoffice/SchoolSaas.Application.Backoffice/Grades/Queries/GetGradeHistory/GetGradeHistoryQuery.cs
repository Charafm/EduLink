using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Common.DataObjects.Grade;

namespace SchoolSaas.Application.Backoffice.Grades.Queries.GetGradeHistory
{
    public class GetGradeHistoryQuery : IRequest<PagedResult<GradeHistoryDTO>>
    {
        public GradeHistoryFilterDTO DTO { get; set; }
    }

    public class GetGradeHistoryQueryHandler : IRequestHandler<GetGradeHistoryQuery, PagedResult<GradeHistoryDTO>>
    {
        private readonly IGradebookService _service;

        public GetGradeHistoryQueryHandler(IGradebookService service)
        {
            _service = service;
        }

        public async Task<PagedResult<GradeHistoryDTO>> Handle(GetGradeHistoryQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetGradeHistoryAsync(request.DTO, cancellationToken);
        }
    }
}