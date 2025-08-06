using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Teacher;

namespace SchoolSaas.Application.Backoffice.Teachers.Queries.GetTeacherProfileDetailed
{
    public class GetTeacherProfileDetailedQuery : IRequest<TeacherProfileDTO>
    {
        public string userId { get; set; }
    }

    public class GetTeacherProfileDetailedHandler : IRequestHandler<GetTeacherProfileDetailedQuery, TeacherProfileDTO>
    {
        private readonly IUserProfileService _teacherService;

        public GetTeacherProfileDetailedHandler(IUserProfileService teacherService)
        {
            _teacherService = teacherService;
        }

        public async Task<TeacherProfileDTO> Handle(GetTeacherProfileDetailedQuery request, CancellationToken cancellationToken)
        {
            return await _teacherService.GetTeacherProfile(request.userId, cancellationToken);
        }
    }
}
