using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Enrollment;

namespace SchoolSaas.Application.Backoffice.Enrollment.Commands.UpdateEnrollmentStatus
{
    public class UpdateEnrollmentStatusCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public EnrollmentStatusUpdateDTO DTO { get; set; }
    }

    public class UpdateEnrollmentStatusCommandHandler : IRequestHandler<UpdateEnrollmentStatusCommand, bool>
    {
        private readonly IEnrollmentService _service;

        public UpdateEnrollmentStatusCommandHandler(IEnrollmentService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(UpdateEnrollmentStatusCommand request, CancellationToken cancellationToken)
        {
            return await _service.UpdateEnrollmentStatusAsync(request.Id,request.DTO,cancellationToken);
        }
    }


}