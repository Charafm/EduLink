using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Student;

namespace SchoolSaas.Application.Backoffice.Students.Queries.GetStudentWithDetails
{
    public class GetStudentWithDetailsQuery : IRequest<StudentDetailDTO>
    {
        public Guid Id { get; set; }
    }

    public class GetStudentWithDetailsQueryHandler : IRequestHandler<GetStudentWithDetailsQuery, StudentDetailDTO>
    {
        private readonly IStudentService _service;

        public GetStudentWithDetailsQueryHandler(IStudentService service)
        {
            _service = service;
        }

        public async Task<StudentDetailDTO> Handle(GetStudentWithDetailsQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetStudentWithDetailsAsync(request.Id, cancellationToken);
        }
    }
}