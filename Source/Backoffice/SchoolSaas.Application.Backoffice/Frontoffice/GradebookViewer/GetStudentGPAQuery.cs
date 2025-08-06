using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Common.DataObjects.Academic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Application.Backoffice.Frontoffice.GradebookViewer
{
   
    public class GetStudentGPAQuery : IRequest<GpaDTO>
    {
        public Guid Id  { get; set; }
    }

    public class GetStudentGPAQueryHandler : IRequestHandler<GetStudentGPAQuery, GpaDTO>
    {
        private readonly IGradeViewerService _service;

        public GetStudentGPAQueryHandler(IGradeViewerService service)
        {
            _service = service;
        }

        public async Task<GpaDTO> Handle(GetStudentGPAQuery request, CancellationToken cancellationToken)
        {
            return await _service.CalculateGPAForStudentAsync(request.Id, cancellationToken);
        }
    }
}
