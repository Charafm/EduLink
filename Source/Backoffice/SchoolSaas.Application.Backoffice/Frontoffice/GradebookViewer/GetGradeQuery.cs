using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Common.DataObjects.Grade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Application.Backoffice.Frontoffice.GradebookViewer
{
    
    public class GetGradeQuery : IRequest<PagedResult<GradeDTO>>
    {
        public GradeFilterDTO Data { get; set; }
    }

    public class GetGradeQueryHandler : IRequestHandler<GetGradeQuery, PagedResult<GradeDTO>>
    {
        private readonly IGradeViewerService _service;

        public GetGradeQueryHandler(IGradeViewerService service)
        {
            _service = service;
        }

        public async Task<PagedResult<GradeDTO>> Handle(GetGradeQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetPaginatedGradesAsync(request.Data, cancellationToken); 
        }
    }
}
