using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Common.DataObjects.Teacher;

namespace SchoolSaas.Application.Backoffice.Schedules.Queries.GetTeacherAssignments
{
    public class GetTeacherAssignmentsQuery : IRequest<PagedResult<TeacherAssignmentDTO>>
    {
        public TeacherAssignmentFilterDTO DTO { get; set; }
    }

    public class GetTeacherAssignmentsQueryHandler : IRequestHandler<GetTeacherAssignmentsQuery, PagedResult<TeacherAssignmentDTO>>
    {
        private readonly IScheduleService _service;

        public GetTeacherAssignmentsQueryHandler(IScheduleService service)
        {
            _service = service;
        }

        public async Task<PagedResult<TeacherAssignmentDTO>> Handle(GetTeacherAssignmentsQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetTeacherAssignmentsAsync(request.DTO,  cancellationToken);
        }
    }
}