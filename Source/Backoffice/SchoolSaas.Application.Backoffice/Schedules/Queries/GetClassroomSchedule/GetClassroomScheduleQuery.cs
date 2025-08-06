using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Schedule;

namespace SchoolSaas.Application.Backoffice.Schedules.Queries.GetClassroomSchedule
{
    public class GetClassroomScheduleQuery : IRequest<List<ScheduleDTO>>
    {
        public Guid Id { get; set; }
    }

    public class GetClassroomScheduleQueryHandler : IRequestHandler<GetClassroomScheduleQuery, List<ScheduleDTO>>
    {
        private readonly IScheduleService _service;

        public GetClassroomScheduleQueryHandler(IScheduleService service)
        {
            _service = service;
        }

        public async Task<List<ScheduleDTO>> Handle(GetClassroomScheduleQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetClassroomScheduleAsync(request.Id, cancellationToken);
        }
    }
}