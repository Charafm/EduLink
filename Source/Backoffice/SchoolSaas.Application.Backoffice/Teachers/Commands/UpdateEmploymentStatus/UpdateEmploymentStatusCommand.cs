using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.Enums;

namespace SchoolSaas.Application.Backoffice.Teachers.Commands.UpdateEmploymentStatus
{
    public class UpdateEmploymentStatusCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public TeacherStatusEnum Data { get; set; }
    }

    public class UpdateEmploymentStatusCommandHandler : IRequestHandler<UpdateEmploymentStatusCommand, bool>
    {
        private readonly ITeacherService _service;

        public UpdateEmploymentStatusCommandHandler(ITeacherService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(UpdateEmploymentStatusCommand request, CancellationToken cancellationToken)
        {
            return await _service.UpdateTeacherEmploymentStatusAsync(request.Id, request.Data, cancellationToken);
        }
    }
}
