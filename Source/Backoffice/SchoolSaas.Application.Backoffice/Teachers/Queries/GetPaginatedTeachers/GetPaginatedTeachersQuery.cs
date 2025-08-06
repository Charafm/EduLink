using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Common.DataObjects.Teacher;

namespace SchoolSaas.Application.Backoffice.Teachers.Queries.GetPaginatedTeachers
{
    public class GetPaginatedTeachersQuery : IRequest<PagedResult<TeacherDTO>>
    {
        public FilterTeacherDTO DTO { get; set; }
    }

    public class GetPaginatedTeachersQueryHandler : IRequestHandler<GetPaginatedTeachersQuery, PagedResult<TeacherDTO>>
    {
        private readonly ITeacherService _service;

        public GetPaginatedTeachersQueryHandler(ITeacherService service)
        {
            _service = service;
        }

        public async Task<PagedResult<TeacherDTO>> Handle(GetPaginatedTeachersQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetPaginatedTeachersAsync(request.DTO, cancellationToken);
        }
    }
}