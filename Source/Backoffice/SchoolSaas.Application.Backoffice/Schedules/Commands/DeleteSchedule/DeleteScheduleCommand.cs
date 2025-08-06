using MediatR;
using SchoolSaas.Application.Common.Interfaces;

namespace SchoolSaas.Application.Backoffice.Schedules.Commands.DeleteSchedule
{
    public class DeleteScheduleCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }

    public class DeleteScheduleCommandHandler : IRequestHandler<DeleteScheduleCommand, bool>
    {
        private readonly IScheduleService _service;

        public DeleteScheduleCommandHandler(IScheduleService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(DeleteScheduleCommand request, CancellationToken cancellationToken)
        {
            return await _service.DeleteScheduleAsync(request.Id,cancellationToken);
        }
    }
}