using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Grade;

namespace SchoolSaas.Application.Backoffice.Grades.Queries.GetGradeStatistics
{
    public class GetGradeStatisticsQuery : IRequest<GradeStatisticsDTO>
    {
        public GradeFilterDTO DTO {  get; set; }
    }

    public class GetGradeStatisticsQueryHandler : IRequestHandler<GetGradeStatisticsQuery, GradeStatisticsDTO>
    {
        private readonly IGradebookService _service;

        public GetGradeStatisticsQueryHandler(IGradebookService service)
        {
            _service = service;
        }

        public async Task<GradeStatisticsDTO> Handle(GetGradeStatisticsQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetGradeStatisticsAsync(request.DTO,cancellationToken);
        }
    }
}