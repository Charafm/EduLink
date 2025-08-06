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
    public class SubmitEnrollmentCommand : IRequest<ResponseDto<bool>>
    {
        public EnrollmentDTO Data { get; set; }
    }

    public class SubmitEnrollmentCommandHandler : IRequestHandler<SubmitEnrollmentCommand, ResponseDto<bool>>
    {
        private readonly IBackofficeConnectedService _service;
        public SubmitEnrollmentCommandHandler(IBackofficeConnectedService service) => _service = service;

        public async Task<ResponseDto<bool>> Handle(SubmitEnrollmentCommand request, CancellationToken cancellationToken)
            => await _service.SubmitEnrollment(request.Data, cancellationToken);
    }
}
