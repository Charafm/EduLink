using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Common.DataObjects.Student;

namespace SchoolSaas.Application.Backoffice.Students.Queries.GetPaginatedStudents
{
    public class GetPaginatedStudentsQuery : IRequest<PagedResult<StudentDTO>>
    {
        public StudentFilterDTO DTO { get; set; }
    }

    public class GetPaginatedStudentsQueryHandler : IRequestHandler<GetPaginatedStudentsQuery, PagedResult<StudentDTO>>
    {
        private readonly IStudentService _service;

        public GetPaginatedStudentsQueryHandler(IStudentService service)
        {
            _service = service;
        }

        public async Task<PagedResult<StudentDTO>> Handle(GetPaginatedStudentsQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetPaginatedStudentsAsync(request.DTO, cancellationToken);
        }
    }
}