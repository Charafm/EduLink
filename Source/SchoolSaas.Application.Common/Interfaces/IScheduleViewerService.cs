using SchoolSaas.Domain.Common.DataObjects.Schedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Application.Common.Interfaces
{
    public interface IScheduleViewerService
    {
        Task<List<ScheduleDTO>> GetScheduleByGradeSectionAsync(Guid gradeSectionId, CancellationToken cancellationToken);
        Task<List<ScheduleDTO>> GetTeacherScheduleAsync(Guid teacherId, CancellationToken cancellationToken);
        Task<List<ScheduleDTO>> GetClassroomScheduleAsync(Guid classroomId, CancellationToken cancellationToken);

    }
}
