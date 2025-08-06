using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Schedule;

namespace SchoolSaas.Application.Backoffice.Schedules.Commands.UpdateSchedule
{
    public class UpdateScheduleCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public ScheduleDTO DTO { get; set; }
    }

    public class UpdateScheduleCommandHandler : IRequestHandler<UpdateScheduleCommand, bool>
    {
        private readonly IScheduleService _service;

        public UpdateScheduleCommandHandler(IScheduleService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(UpdateScheduleCommand request, CancellationToken cancellationToken)
        {
            return await _service.UpdateScheduleAsync(request.Id, request.DTO, cancellationToken);
        }
    }
}