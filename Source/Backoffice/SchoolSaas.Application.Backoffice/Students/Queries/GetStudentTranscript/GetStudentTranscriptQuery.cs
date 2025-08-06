using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Student;

namespace SchoolSaas.Application.Backoffice.Students.Queries.GetStudentTranscript
{
    public class GetStudentTranscriptQuery : IRequest<StudentTranscriptDTO>
    {
        public Guid StudentId { get; set; }
    }
    public class GetStudentTranscriptQueryHandler : IRequestHandler<GetStudentTranscriptQuery, StudentTranscriptDTO>
    {
        private readonly IStudentService _studentService;

        public GetStudentTranscriptQueryHandler(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public async Task<StudentTranscriptDTO> Handle(GetStudentTranscriptQuery request, CancellationToken cancellationToken)
        {
            return await _studentService.GetStudentTranscriptAsync(request.StudentId, cancellationToken);
        }
    }
}
