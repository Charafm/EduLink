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
    
    
    public class GetCourseDetailsQuery : IRequest<CourseDetailDTO>
    {
            public Guid Id { get; set; }
    }

    public class GetCourseDetailsQueryHandler : IRequestHandler<GetCourseDetailsQuery, CourseDetailDTO>
    {
        private readonly ICourseViewerService _service;

        public GetCourseDetailsQueryHandler(ICourseViewerService service)
        {
            _service = service;
        }

        public async Task<CourseDetailDTO> Handle(GetCourseDetailsQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetCourseDetailsAsync(request.Id, cancellationToken);
        }
    }
}
    