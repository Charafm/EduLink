using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Student;

namespace SchoolSaas.Application.Backoffice.Profile
{
    public class GetStudentDashboardQuery : IRequest<StudentDashboardDTO>
    {
        public Guid StudentId { get; set; }
    }
    public class GetStudentDashboardQueryHandler : IRequestHandler<GetStudentDashboardQuery, StudentDashboardDTO>
    {
        private readonly IUserProfileService _studentService;

        public GetStudentDashboardQueryHandler(IUserProfileService studentService)
        {
            _studentService = studentService;
        }

        public async Task<StudentDashboardDTO> Handle(GetStudentDashboardQuery request, CancellationToken cancellationToken)
        {
            return await _studentService.GetStudentDashboardAsync(request.StudentId, cancellationToken);
        }
    }
}
