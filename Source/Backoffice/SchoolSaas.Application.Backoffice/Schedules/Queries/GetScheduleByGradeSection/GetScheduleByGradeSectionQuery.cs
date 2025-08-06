using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Schedule;

namespace SchoolSaas.Application.Backoffice.Schedules.Queries.GetScheduleByGradeSection
{
    public class GetScheduleByGradeSectionQuery : IRequest<List<ScheduleDTO>>
    {
        public Guid Id { get; set; }
    }

    public class GetScheduleByGradeSectionQueryHandler : IRequestHandler<GetScheduleByGradeSectionQuery, List<ScheduleDTO>>
    {
        private readonly IScheduleService _service;

        public GetScheduleByGradeSectionQueryHandler(IScheduleService service)
        {
            _service = service;
        }

        public async Task<List<ScheduleDTO>> Handle(GetScheduleByGradeSectionQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetScheduleByGradeSectionAsync(request.Id, cancellationToken);
        }
    }
}