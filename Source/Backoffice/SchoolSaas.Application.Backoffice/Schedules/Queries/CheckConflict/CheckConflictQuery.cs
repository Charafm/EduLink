using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Schedule;

namespace SchoolSaas.Application.Backoffice.Schedules.Queries.CheckConflict
{
    public class CheckConflictQuery : IRequest<bool>
    {
        public ScheduleConflictCheckDTO DTO { get; set; }
    }

    public class CheckConflictQueryHandler : IRequestHandler<CheckConflictQuery, bool>
    {
        private readonly IScheduleService _service;

        public CheckConflictQueryHandler(IScheduleService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(CheckConflictQuery request, CancellationToken cancellationToken)
        {
            return await _service.CheckScheduleConflictsAsync(request.DTO,cancellationToken);
        }
    }
}