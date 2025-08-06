using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Common.DataObjects.Enrollment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Application.Frontoffice.Enrollment
{
    public class GetEnrollmentTranscriptQuery : IRequest<EnrollmentTranscriptDTO>
    {
        public Guid Id { get; set; }
    }

    public class GetEnrollmentTranscriptQueryHandler : IRequestHandler<GetEnrollmentTranscriptQuery, EnrollmentTranscriptDTO>
    {
        private readonly IBackofficeConnectedService _service;
        public GetEnrollmentTranscriptQueryHandler(IBackofficeConnectedService service) => _service = service;

        public async Task<EnrollmentTranscriptDTO> Handle(GetEnrollmentTranscriptQuery request, CancellationToken cancellationToken)
            =>  _service.GetEnrollmentTranscript(request.Id, cancellationToken).Result.Data;
    }
}
