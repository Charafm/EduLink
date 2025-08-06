using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Schedule;

namespace SchoolSaas.Application.Backoffice.Schedules.Queries.GetTeacherSchedule
{
    public class GetTeacherScheduleQuery : IRequest<List<ScheduleDTO>>
    {
        public Guid Id  { get; set; }
    }

    public class GetTeacherScheduleQueryHandler : IRequestHandler<GetTeacherScheduleQuery, List<ScheduleDTO>>
    {
        private readonly IScheduleService _service;

        public GetTeacherScheduleQueryHandler(IScheduleService service)
        {
            _service = service;
        }

        public async Task<List<ScheduleDTO>> Handle(GetTeacherScheduleQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetTeacherScheduleAsync(request.Id, cancellationToken);   
        }
    }
}