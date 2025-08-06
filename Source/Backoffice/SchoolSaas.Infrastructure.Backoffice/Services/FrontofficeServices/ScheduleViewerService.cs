using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Schedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Infrastructure.Backoffice.Services.FrontofficeServices
{
    public  class ScheduleViewerService : IScheduleViewerService
    {
        private readonly IScheduleService _service;

        public ScheduleViewerService(IScheduleService service)
        {
            _service = service;
        }
        public async Task<List<ScheduleDTO>> GetScheduleByGradeSectionAsync(Guid gradeSectionId, CancellationToken cancellationToken)
        {
            return await _service.GetScheduleByGradeSectionAsync(gradeSectionId, cancellationToken);
        }
        public async Task<List<ScheduleDTO>> GetTeacherScheduleAsync(Guid teacherId, CancellationToken cancellationToken)
        {
            return await _service.GetTeacherScheduleAsync(teacherId, cancellationToken);
        }
        public async Task<List<ScheduleDTO>> GetClassroomScheduleAsync(Guid classroomId, CancellationToken cancellationToken)
        {
            return await _service.GetClassroomScheduleAsync(classroomId, cancellationToken);
        }
    }
}
