using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Enrollment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Application.Backoffice.Frontoffice.EnrollmentPortalService
{
  
    public class SubmitEnrollmentCommand : IRequest<bool>
    {
        public EnrollmentDTO Data { get; set; }
    }

    public class SubmitEnrollmentCommandHandler : IRequestHandler<SubmitEnrollmentCommand, bool>
    {
        private readonly IEnrollmentPortalService _service;

        public SubmitEnrollmentCommandHandler(IEnrollmentPortalService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(SubmitEnrollmentCommand request, CancellationToken cancellationToken)
        {
            return await _service.SubmitEnrollmentAsync(request.Data, cancellationToken);
        }
    }

}
