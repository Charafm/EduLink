using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Teacher;

namespace SchoolSaas.Application.Backoffice.Teachers.Queries.GetTeacherByUserId
{
    public class GetTeacherByUserIdQuery : IRequest<TeacherDTO>
    {
        public string userId { get; set; }
    }

    public class GetTeacherByUserIdQueryHandler : IRequestHandler<GetTeacherByUserIdQuery, TeacherDTO>
    {
        private readonly ITeacherService _service;

        public GetTeacherByUserIdQueryHandler(ITeacherService service)
        {
            _service = service;
        }

        public async Task<TeacherDTO> Handle(GetTeacherByUserIdQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetTeacherByUserId(request.userId);
        }
    }
}
