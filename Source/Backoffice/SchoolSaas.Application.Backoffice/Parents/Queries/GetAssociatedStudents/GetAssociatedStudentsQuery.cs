using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Common.DataObjects.Student;

namespace SchoolSaas.Application.Backoffice.Parents.Queries.GetAssociatedStudents
{
    public class GetAssociatedStudentsQuery : IRequest<PagedResult<ParsedStudentDto>>
    {
        public Guid Id { get; set; }
        public int page  { get; set; }
        public int? size { get; set; }
    }

    public class GetAssociatedStudentsQueryHandler : IRequestHandler<GetAssociatedStudentsQuery, PagedResult<ParsedStudentDto>>
    {
        private readonly IParentService _service;

        public GetAssociatedStudentsQueryHandler(IParentService service)
        {
            _service = service;
        }

        public async Task<PagedResult<ParsedStudentDto>> Handle(GetAssociatedStudentsQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetAssociatedStudentsAsync(request.Id, request.page, request.size, cancellationToken);
        }
    }
}