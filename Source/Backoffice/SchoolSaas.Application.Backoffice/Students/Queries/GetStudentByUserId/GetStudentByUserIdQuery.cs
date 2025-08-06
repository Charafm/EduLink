using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Student;

namespace SchoolSaas.Application.Backoffice.Students.Queries.GetStudentByUserId
{
    public class GetStudentByUserIdQuery : IRequest<StudentDTO>
    {
        public string userId { get; set; }
    }

    public class GetStudentByUserIdQueryHandler : IRequestHandler<GetStudentByUserIdQuery, StudentDTO>
    {
        private readonly IStudentService _service;

        public GetStudentByUserIdQueryHandler(IStudentService service)
        {
            _service = service;
        }

        public async Task<StudentDTO> Handle(GetStudentByUserIdQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetStudentByUserId(request.userId);
        }
    }
}
