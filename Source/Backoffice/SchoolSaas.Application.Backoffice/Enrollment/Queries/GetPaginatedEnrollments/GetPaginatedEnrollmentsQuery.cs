using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Common.DataObjects.Enrollment;

namespace SchoolSaas.Application.Backoffice.Enrollment.Queries.GetPaginatedEnrollments
{
    public class GetPaginatedEnrollmentsQuery : IRequest<PagedResult<EnrollmentDTO>>
    {
        public EnrollmentFilterDTO DTO { get; set; }
    }

    public class GetPaginatedEnrollmentsQueryHandler : IRequestHandler<GetPaginatedEnrollmentsQuery, PagedResult<EnrollmentDTO>>
    {
        private readonly IEnrollmentService _service;

        public GetPaginatedEnrollmentsQueryHandler(IEnrollmentService service)
        {
            _service = service;
        }

        public async Task<PagedResult<EnrollmentDTO>> Handle(GetPaginatedEnrollmentsQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetPaginatedEnrollmentsAsync(request.DTO,cancellationToken);
        }
    }
}