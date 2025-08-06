using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Common.DataObjects.Grade;
using SchoolSaas.Domain.Common.DataObjects.SchoolSupply;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Application.Backoffice.Frontoffice.ResourceViewer
{
   
    public class GetGraderResourcesQuery : IRequest<PagedResult<GradeResourceDTO>>
    {
        public ResourceFilterDTO Data {  get; set; }
    }

    public class GetGraderResourcesQueryHandler : IRequestHandler<GetGraderResourcesQuery, PagedResult<GradeResourceDTO>>
    {
        private readonly IResourceViewerService _service;

        public GetGraderResourcesQueryHandler(IResourceViewerService service)
        {
            _service = service;
        }

        public async Task<PagedResult<GradeResourceDTO>> Handle(GetGraderResourcesQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetPaginatedResourcesAsync(request.Data, cancellationToken);  
        }
    }
}
