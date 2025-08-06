using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Common.DataObjects.Grade;
using SchoolSaas.Domain.Common.DataObjects.SchoolSupply;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Application.Common.Interfaces
{
    public interface IResourceViewerService
    {
        Task<PagedResult<GradeResourceDTO>> GetPaginatedResourcesAsync(ResourceFilterDTO filter, CancellationToken cancellationToken);

    }
}
