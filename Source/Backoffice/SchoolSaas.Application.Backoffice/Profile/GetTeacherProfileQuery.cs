using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Common.DataObjects.Teacher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Application.Backoffice.Profile
{
   
    public class GetTeacherProfileQuery : IRequest<TeacherProfileDTO>
    {
        public string userId    { get; set; }
    }

    public class GetTeacherProfileQueryHandler : IRequestHandler<GetTeacherProfileQuery, TeacherProfileDTO>
    {
        private readonly IUserProfileService _service;

        public GetTeacherProfileQueryHandler(IUserProfileService service) { 
        
            _service = service;
        }

        public async Task<TeacherProfileDTO> Handle(GetTeacherProfileQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetTeacherProfile(request.userId, cancellationToken);
        }
    }
}
