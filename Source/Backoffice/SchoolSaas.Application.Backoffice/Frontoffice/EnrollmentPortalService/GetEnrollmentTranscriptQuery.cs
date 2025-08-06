using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Common.DataObjects.Enrollment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Application.Backoffice.Frontoffice.EnrollmentPortalService
{
   
    public class GetEnrollmentTranscriptQuery : IRequest<EnrollmentTranscriptDTO>
    {
        public Guid Id { get; set; }    
    }

    public class QueryHandler : IRequestHandler<GetEnrollmentTranscriptQuery, EnrollmentTranscriptDTO>
    {
        private readonly IEnrollmentPortalService _service;

        public QueryHandler(IEnrollmentPortalService service)
        {
            _service = service;
        }

        public async Task<EnrollmentTranscriptDTO> Handle(GetEnrollmentTranscriptQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetEnrollmentTranscriptAsync( request.Id, cancellationToken);
        }
    }
}
