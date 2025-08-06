using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Common.DataObjects.Student;

namespace SchoolSaas.Application.Backoffice.Students.Queries.GetStudentParents
{
    public class GetStudentParentsQuery : IRequest<PagedResult<StudentParentDTO>>
    {
        public Guid Id { get; set; }
    }

    public class GetStudentParentsQueryHandler : IRequestHandler<GetStudentParentsQuery, PagedResult<StudentParentDTO>>
    {
        private readonly IStudentService _service;

        public GetStudentParentsQueryHandler(IStudentService service)
        {
            _service = service;
        }

        public async Task<PagedResult<StudentParentDTO>> Handle(GetStudentParentsQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetStudentParentsAsync(request.Id , cancellationToken);
        }
    }
}