using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Schedule;

namespace SchoolSaas.Application.Backoffice.Schedules.Commands.AutoGenerateSchedules
{
    public class AutoGenerateSchedulesCommand : IRequest<List<ScheduleDTO>>
    {
        public ScheduleConstraintsDTO DTO { get; set; }
    }

    public class AutoGenerateSchedulesCommandHandler : IRequestHandler<AutoGenerateSchedulesCommand, List<ScheduleDTO>>
    {
        private readonly IScheduleService _service;

        public AutoGenerateSchedulesCommandHandler(IScheduleService service)
        {
            _service = service;
        }

        public async Task<List<ScheduleDTO>> Handle(AutoGenerateSchedulesCommand request, CancellationToken cancellationToken)
        {
            return await _service.AutoGenerateSchedulesAsync(request.DTO,cancellationToken);
        }
    }
}