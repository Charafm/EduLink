using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Enrollment;

namespace SchoolSaas.Application.Backoffice.Enrollment.Commands.BulkUpdateEnrollmentStatus
{
    public class BulkUpdateEnrollmentStatusCommand : IRequest<bool>
    {
        public BulkEnrollmentStatusUpdateDTO DTO
        {
            get; set;
        }

        public class BulkUpdateEnrollmentStatusCommandHandler : IRequestHandler<BulkUpdateEnrollmentStatusCommand, bool>
        {
            private readonly IEnrollmentService _service;

            public BulkUpdateEnrollmentStatusCommandHandler(IEnrollmentService service)
            {
                _service = service;
            }

            public async Task<bool> Handle(BulkUpdateEnrollmentStatusCommand request, CancellationToken cancellationToken)
            {
                return await _service.BulkUpdateEnrollmentStatusAsync(request.DTO, cancellationToken);
            }
        }


    }
}