using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Common;
using SchoolSaas.Domain.Common.DataObjects.Enrollment;

namespace SchoolSaas.Application.Backoffice.Enrollment.Queries.GetEnrollmentDashboard
{
    public class GetEnrollmentDashboardQuery : IRequest<EnrollmentMetricsDTO>
    {
        public DateRangeDTO DTO { get; set; }
    }

    public class GetEnrollmentDashboardQueryHandler : IRequestHandler<GetEnrollmentDashboardQuery, EnrollmentMetricsDTO>
    {
        private readonly IEnrollmentService _service;

        public GetEnrollmentDashboardQueryHandler(IEnrollmentService service)
        {
            _service = service;
        }

        public async Task<EnrollmentMetricsDTO> Handle(GetEnrollmentDashboardQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetEnrollmentDashboardMetricsAsync(request.DTO,cancellationToken);
        }
    }
}