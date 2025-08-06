using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Common.DataObjects.Enrollment;

namespace SchoolSaas.Application.Backoffice.Enrollment.Queries.GetEnrollmentHistory
{
    public class GetEnrollmentHistoryQuery : IRequest<PagedResult<EnrollmentStatusHistoryDTO>>
    {
        public EnrollmentHistoryFilterDTO DTO { get; set; }
    }

    public class GetEnrollmentHistoryQueryHandler : IRequestHandler<GetEnrollmentHistoryQuery, PagedResult<EnrollmentStatusHistoryDTO>>
    {
        private readonly IEnrollmentService _service;

        public GetEnrollmentHistoryQueryHandler(IEnrollmentService service)
        {
            _service = service;
        }

        public async Task<PagedResult<EnrollmentStatusHistoryDTO>> Handle(GetEnrollmentHistoryQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetEnrollmentHistoryAsync(request.DTO,cancellationToken);
        }
    }
}