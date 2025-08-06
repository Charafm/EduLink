using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Academic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Application.Frontoffice.Academics
{
    public class GetStudentGPAQuery : IRequest<GpaDTO>
    {
        public Guid StudentId { get; set; }
    }

    public class GetStudentGPAQueryHandler : IRequestHandler<GetStudentGPAQuery, GpaDTO>
    {
        private readonly IBackofficeConnectedService _service;

        public GetStudentGPAQueryHandler(IBackofficeConnectedService service) => _service = service;

        public async Task<GpaDTO> Handle(GetStudentGPAQuery request, CancellationToken cancellationToken)
        {
            return (await _service.GetStudentGPA(request.StudentId, cancellationToken)).Data;
        }
    }

}
