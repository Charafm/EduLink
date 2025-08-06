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
  
    public class GetStaffProfileQuery : IRequest<StaffProfileDTO>
    {
        public string userId { get; set; }
    }

    public class GetStaffProfileQueryHandler : IRequestHandler<GetStaffProfileQuery, StaffProfileDTO>
    {
        private readonly IUserProfileService _service;

        public GetStaffProfileQueryHandler(IUserProfileService service)
        {
            _service = service;
        }

        public async Task<StaffProfileDTO> Handle(GetStaffProfileQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetStaffProfile(request.userId, cancellationToken);
        }
    }
}
