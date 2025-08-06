using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Common.DataObjects.Schedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Application.Backoffice.Frontoffice.ScheduleViewer
{
   
    public class GetScheduleByGradeSectionQuery : IRequest<List<ScheduleDTO>>
    {
        public Guid Id { get; set; }
    }

    public class GetScheduleByGradeSectionQueryHandler : IRequestHandler<GetScheduleByGradeSectionQuery, List<ScheduleDTO>>
    {
        private readonly IScheduleViewerService _service;

        public GetScheduleByGradeSectionQueryHandler(IScheduleViewerService service)
        {
            _service = service;
        }

        public async Task<List<ScheduleDTO>> Handle(GetScheduleByGradeSectionQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetScheduleByGradeSectionAsync(request.Id, cancellationToken);
        }
    }
}
