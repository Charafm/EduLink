using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Common.DataObjects.Staff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Application.Backoffice.Profile
{
   
    public class GetStaffDashboardQuery : IRequest<StaffDashboardDTO>
    {
        public Guid Id { get; set; }
    }

    public class GetStaffDashboardHandler : IRequestHandler<GetStaffDashboardQuery, StaffDashboardDTO>
    {
        private readonly IUserProfileService _service;

        public GetStaffDashboardHandler(IUserProfileService service)
        {
            _service = service;
        }

        public async Task<StaffDashboardDTO> Handle(GetStaffDashboardQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetStaffDashboardAsync(request.Id , cancellationToken);
        }
    }
}
