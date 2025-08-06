using MediatR;
using SchoolSaas.Application.Common.Interfaces;

namespace SchoolSaas.Application.Backoffice.Schedules.Commands.Reschedule
{
    public class RescheduleCommand : IRequest<bool>
    {
        public Guid ScheduleId { get; set; }
        public TimeOnly NewStartTime { get; set; }
        public TimeOnly NewEndTime { get; set; }
    }
    public class RescheduleCommandHandler : IRequestHandler<RescheduleCommand, bool>
    {
        private readonly IScheduleService _svc;
        public RescheduleCommandHandler(IScheduleService svc) => _svc = svc;

        public Task<bool> Handle(RescheduleCommand req, CancellationToken ct) =>
            _svc.RescheduleAsync(req.ScheduleId, req.NewStartTime, req.NewEndTime, ct);
    }
}
