using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Common.DataObjects.Student;

namespace SchoolSaas.Application.Backoffice.Students.Queries.GetStudentHistory
{
    public class GetStudentHistoryQuery : IRequest<PagedResult<StudentHistoryDTO>>
    {
        public StudentHistoryFilterDTO DTO { get; set; }
    }

    public class GetStudentHistoryQueryHandler : IRequestHandler<GetStudentHistoryQuery, PagedResult<StudentHistoryDTO>>
    {
        private readonly IStudentService _service;

        public GetStudentHistoryQueryHandler(IStudentService service)
        {
            _service = service;
        }

        public async Task<PagedResult<StudentHistoryDTO>> Handle(GetStudentHistoryQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetStudentHistoryAsync(request.DTO, cancellationToken);
        }
    }
}