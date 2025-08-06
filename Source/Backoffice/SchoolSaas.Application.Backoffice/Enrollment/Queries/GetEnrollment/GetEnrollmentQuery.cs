using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Enrollment;

namespace SchoolSaas.Application.Backoffice.Enrollment.Queries.GetEnrollment
{
    public class GetEnrollmentQuery : IRequest<EnrollmentDetailDTO>
    {
        public Guid Id { get; set; }
    }

    public class GetEnrollmentQueryHandler : IRequestHandler<GetEnrollmentQuery, EnrollmentDetailDTO>
    {
        private readonly IEnrollmentService _service;

        public GetEnrollmentQueryHandler(IEnrollmentService service)
        {
            _service = service;
        }

        public async Task<EnrollmentDetailDTO> Handle(GetEnrollmentQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetEnrollmentAsync(request.Id, cancellationToken);
        }
    }
}