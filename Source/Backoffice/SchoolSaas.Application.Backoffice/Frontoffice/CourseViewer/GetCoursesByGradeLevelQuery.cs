using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Common.DataObjects.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Application.Backoffice.Frontoffice.CourseViewer
{
   

    public class GetCoursesByGradeLevelQuery : IRequest<List<CourseDTO>>
    {
        public Guid Id { get; set; }
    }

    public class GetCoursesByGradeLevelQueryHandler : IRequestHandler<GetCoursesByGradeLevelQuery, List<CourseDTO>>
    {
        private readonly ICourseViewerService _service;

        public GetCoursesByGradeLevelQueryHandler(ICourseViewerService service)
        {
            _service = service;
        }

        public async Task<List<CourseDTO>> Handle(GetCoursesByGradeLevelQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetCoursesByGradeLevelAsync(request.Id, cancellationToken); 
        }
    }
}
