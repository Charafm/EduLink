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
    public class GetCurrentYearQuery : IRequest<AcademicYearDTO>
    {
    }
    public class GetCurrentYearQueryHandler : IRequestHandler<GetCurrentYearQuery, AcademicYearDTO>
    {
        private readonly IBackofficeConnectedService _service;
        public GetCurrentYearQueryHandler(IBackofficeConnectedService service) => _service = service;
        public async Task<AcademicYearDTO> Handle(GetCurrentYearQuery request, CancellationToken ct) =>
            (await _service.GetCurrentYear(ct)).Data;
    }
}
