using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Common;
using SchoolSaas.Domain.Common.DataObjects.Schedule;

namespace SchoolSaas.Application.Backoffice.Schedules.Queries.GetScheduleDashboardMetrics
{
    public class GetScheduleDashboardMetricsQuery : IRequest<ScheduleDashboardMetricsDTO>
    {
        public DateRangeDTO DateRange { get; set; }
    }
    public class GetScheduleDashboardMetricsQueryHandler
    : IRequestHandler<GetScheduleDashboardMetricsQuery, ScheduleDashboardMetricsDTO>
    {
        private readonly IScheduleService _svc;
        public GetScheduleDashboardMetricsQueryHandler(IScheduleService svc) => _svc = svc;

        public Task<ScheduleDashboardMetricsDTO> Handle(GetScheduleDashboardMetricsQuery req, CancellationToken ct) =>
            _svc.GetScheduleDashboardMetricsAsync(req.DateRange, ct);
    }
}
