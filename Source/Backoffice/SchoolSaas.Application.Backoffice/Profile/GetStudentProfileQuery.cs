using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Common.DataObjects.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Application.Backoffice.Profile
{
 
    public class GetStudentProfileQuery : IRequest<StudentProfileDTO>
    {
        public string userId { get; set; }
    }

    public class GetStudentProfileQueryHandler : IRequestHandler<GetStudentProfileQuery, StudentProfileDTO>
    {
        private readonly IUserProfileService _service;

        public GetStudentProfileQueryHandler(IUserProfileService service)
        {
            _service = service;
        }

        public async Task<StudentProfileDTO> Handle(GetStudentProfileQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetStudentProfile(request.userId, cancellationToken);
        }
    }
}
