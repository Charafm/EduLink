using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Application.Frontoffice.Course
{
    public class GetCourseDetailsQuery : IRequest<CourseDetailDTO>
    {
        public Guid CourseId { get; set; }
    }

    public class GetCourseDetailsQueryHandler : IRequestHandler<GetCourseDetailsQuery, CourseDetailDTO>
    {
        private readonly IBackofficeConnectedService _service;

        public GetCourseDetailsQueryHandler(IBackofficeConnectedService service) => _service = service;

        public async Task<CourseDetailDTO> Handle(GetCourseDetailsQuery request, CancellationToken cancellationToken)
        {
            return (await _service.GetCourseDetails(request.CourseId, cancellationToken)).Data;
        }
    }
}
