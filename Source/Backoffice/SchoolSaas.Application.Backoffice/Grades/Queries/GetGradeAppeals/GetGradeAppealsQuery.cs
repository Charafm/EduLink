using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Common.DataObjects.Grade;

namespace SchoolSaas.Application.Backoffice.Grades.Queries.GetGradeAppeals
{
    public class GetGradeAppealsQuery : IRequest<PagedResult<GradeAppealDTO>>
    {
        public GradeAppealFilterDTO DTO { get; set; }
    }

    public class GetGradeAppealsQueryHandler : IRequestHandler<GetGradeAppealsQuery, PagedResult<GradeAppealDTO>>
    {
        private readonly IGradebookService _service;

        public GetGradeAppealsQueryHandler(IGradebookService service)
        {
            _service = service;
        }

        public async Task<PagedResult<GradeAppealDTO>> Handle(GetGradeAppealsQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetGradeAppealsAsync(request.DTO,cancellationToken);
        }
    }
}