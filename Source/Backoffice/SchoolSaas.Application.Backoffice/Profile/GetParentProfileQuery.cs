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
   
    public class GetParentProfileQuery : IRequest<ParentProfileDTO>
    {
       public string userId { get; set; }
    }

    public class GetParentProfileQueryHandler : IRequestHandler<GetParentProfileQuery, ParentProfileDTO>
    {
        private readonly IUserProfileService _service;

        public GetParentProfileQueryHandler(IUserProfileService service)
        {
            _service = service;
        }

        public async Task<ParentProfileDTO> Handle(GetParentProfileQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetParentProfile(request.userId, cancellationToken);
        }
    }
}
