using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Teacher;

namespace SchoolSaas.Application.Backoffice.Teachers.Queries.GetTeacherById
{
    public class GetTeacherByIdQuery : IRequest<TeacherDTO>
    {
        public Guid Id { get; set; }
    }

    public class GetTeacherByIdQueryHandler : IRequestHandler<GetTeacherByIdQuery, TeacherDTO>
    {
        private readonly ITeacherService _service;

        public GetTeacherByIdQueryHandler(ITeacherService service)
        {
            _service = service;
        }

        public async Task<TeacherDTO> Handle(GetTeacherByIdQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetTeacherByIdAsync(request.Id, cancellationToken);
        }
    }
}