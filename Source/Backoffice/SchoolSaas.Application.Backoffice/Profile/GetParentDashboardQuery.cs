using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Common.DataObjects.Parent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Application.Backoffice.Profile
{
    
    public class GetParentDashboardQuery : IRequest<ParentDashboardDTO>
    {
        public Guid Id { get; set; }
    }

    public class GetParentDashboardQueryHandler : IRequestHandler<GetParentDashboardQuery, ParentDashboardDTO>
    {
        private readonly IUserProfileService _service;

        public GetParentDashboardQueryHandler(IUserProfileService service)
        {
            _service = service;
        }

        public async Task<ParentDashboardDTO> Handle(GetParentDashboardQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetParentDashboardAsync(request.Id, cancellationToken);   
        }
    }
}
