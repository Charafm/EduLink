using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Schedule;

namespace SchoolSaas.Application.Backoffice.Schedules.Commands.CreateSchedule
{
    public class CreateScheduleCommand : IRequest<bool>
    {
        public ScheduleDTO DTO { get; set; }
    }

    public class CreateScheduleCommandHandler : IRequestHandler<CreateScheduleCommand, bool>
    {
        private readonly IScheduleService _service;

        public CreateScheduleCommandHandler(IScheduleService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(CreateScheduleCommand request, CancellationToken cancellationToken)
        {
            return await _service.CreateScheduleAsync(request.DTO, cancellationToken);
        }
    }
}