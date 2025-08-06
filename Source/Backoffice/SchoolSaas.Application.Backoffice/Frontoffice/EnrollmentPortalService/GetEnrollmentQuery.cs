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
 
    public class GetEnrollmentQuery : IRequest<EnrollmentDetailDTO>
    {
        public Guid Id { get; set; }
    }

    public class GetEnrollmentQueryHandler : IRequestHandler<GetEnrollmentQuery, EnrollmentDetailDTO>
    {
        private readonly IEnrollmentPortalService _service;

        public GetEnrollmentQueryHandler(IEnrollmentPortalService service)
        {
            _service = service;
        }

        public async Task<EnrollmentDetailDTO> Handle(GetEnrollmentQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetEnrollmentAsync(request.Id , cancellationToken);
        }
    }
}
