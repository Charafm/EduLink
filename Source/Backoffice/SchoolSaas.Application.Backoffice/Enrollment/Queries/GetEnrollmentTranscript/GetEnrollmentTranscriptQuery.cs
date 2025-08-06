using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Enrollment;

namespace SchoolSaas.Application.Backoffice.Enrollment.Queries.GetEnrollmentTranscript
{
    public class GetEnrollmentTranscriptQuery : IRequest<EnrollmentTranscriptDTO>
    {
        public Guid Id { get; set; }
    }

    public class GetEnrollmentTranscriptQueryHandler : IRequestHandler<GetEnrollmentTranscriptQuery, EnrollmentTranscriptDTO>
    {
        private readonly IEnrollmentService _service;

        public GetEnrollmentTranscriptQueryHandler(IEnrollmentService service)
        {
            _service = service;
        }

        public async Task<EnrollmentTranscriptDTO> Handle(GetEnrollmentTranscriptQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetEnrollmentTranscriptAsync(request.Id, cancellationToken);
        }
    }
}
