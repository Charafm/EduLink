using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Common.DataObjects.Academic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Application.Backoffice.Frontoffice.AcademicsVIewer
{
    
    public class GetAcademicCurrentYearQuery : IRequest<AcademicYearDTO>
    {

    }

    public class GetAcademicCurrentYearQueryHandler : IRequestHandler<GetAcademicCurrentYearQuery, AcademicYearDTO>
    {
        private readonly IAcademicsViewerService _service;

        public GetAcademicCurrentYearQueryHandler(IAcademicsViewerService service)
        {
            _service = service;
        }

        public async Task<AcademicYearDTO> Handle(GetAcademicCurrentYearQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetCurrentAcademicYearAsync();
        }
    }
}
