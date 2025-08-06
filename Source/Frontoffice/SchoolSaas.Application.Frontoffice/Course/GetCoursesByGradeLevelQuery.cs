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
    public class GetCoursesByGradeLevelQuery : IRequest<ICollection<CourseDTO>>
    {
        public Guid GradeLevelId { get; set; }
    }

    public class GetCoursesByGradeLevelQueryHandler : IRequestHandler<GetCoursesByGradeLevelQuery, ICollection<CourseDTO>>
    {
        private readonly IBackofficeConnectedService _service;

        public GetCoursesByGradeLevelQueryHandler(IBackofficeConnectedService service) => _service = service;

        public async Task<ICollection<CourseDTO>> Handle(GetCoursesByGradeLevelQuery request, CancellationToken cancellationToken)
        {
            return (await _service.GetCoursesByGradeLevel(request.GradeLevelId, cancellationToken)).Data;
        }
    }

}
