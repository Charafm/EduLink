using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Common.DataObjects.Grade;
using SchoolSaas.Domain.Common.DataObjects.SchoolSupply;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Infrastructure.Backoffice.Services.FrontofficeServices
{
    public class ResourceViewerService : IResourceViewerService
    {
        private readonly ISchoolSupplyService _service;

        public ResourceViewerService(ISchoolSupplyService service)
        {
            _service = service;
        }
        public async Task<PagedResult<GradeResourceDTO>> GetPaginatedResourcesAsync(ResourceFilterDTO filter, CancellationToken cancellationToken)
        {
            return await _service.GetPaginatedResourcesAsync(filter, cancellationToken).ConfigureAwait(false);
        }
    }
}
