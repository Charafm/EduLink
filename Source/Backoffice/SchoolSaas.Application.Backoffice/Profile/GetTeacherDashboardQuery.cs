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
   
    public class GetTeacherDashboardQuery : IRequest<TeacherDashboardDTO>
    {
        public Guid Id  { get; set; }
    }

    public class GetTeacherDashboardQueryHandler : IRequestHandler<GetTeacherDashboardQuery, TeacherDashboardDTO>
    {
        private readonly IUserProfileService _service;

        public GetTeacherDashboardQueryHandler(IUserProfileService service)
        {
            _service = service;
        }

        public async Task<TeacherDashboardDTO> Handle(GetTeacherDashboardQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetTeacherDashboardAsync(request.Id, cancellationToken);  
        }
    }
}
