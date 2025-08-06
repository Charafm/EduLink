using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Common.DataObjects.Grade;

namespace SchoolSaas.Application.Backoffice.Grades.Queries.GetGradesByTeacher
{
    public class GetGradesByTeacherQuery : IRequest<PagedResult<GradeDTO>>
    {
        public Guid Id { get; set; }
        public GradeFilterDTO DTO { get; set; }
    }

    public class GetGradesByTeacherQueryHandler : IRequestHandler<GetGradesByTeacherQuery, PagedResult<GradeDTO>>
    {
        private readonly IGradebookService _service;

        public GetGradesByTeacherQueryHandler(IGradebookService service)
        {
            _service = service;
        }

        public async Task<PagedResult<GradeDTO>> Handle(GetGradesByTeacherQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetGradesByTeacherAsync(request.Id,request.DTO,cancellationToken);
        }
    }
}