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
    public class GetEnrollmentQuery : IRequest<EnrollmentDetailDTO>
    {
        public Guid Id { get; set; }
    }

    public class GetEnrollmentQueryHandler : IRequestHandler<GetEnrollmentQuery, EnrollmentDetailDTO>
    {
        private readonly IBackofficeConnectedService _service;
        public GetEnrollmentQueryHandler(IBackofficeConnectedService service) => _service = service;

        public async Task<EnrollmentDetailDTO> Handle(GetEnrollmentQuery request, CancellationToken cancellationToken)
            =>  _service.GetEnrollment(request.Id, cancellationToken).Result.Data;
    }
}
