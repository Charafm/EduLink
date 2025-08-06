using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Common.DataObjects.Grade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Application.Backoffice.Frontoffice.GradebookViewer
{
  
    public class GetGradeStatisticsQuery : IRequest<GradeStatisticsDTO>
    {
        public GradeFilterDTO Data { get; set; }
    }

    public class GetGradeStatisticsQueryHandler : IRequestHandler<GetGradeStatisticsQuery, GradeStatisticsDTO>
    {
        private readonly IGradeViewerService _service;

        public GetGradeStatisticsQueryHandler(IGradeViewerService service)
        {
            _service = service;
        }

        public async Task<GradeStatisticsDTO> Handle(GetGradeStatisticsQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetGradeStatisticsAsync(request.Data, cancellationToken);
        }
    }
}
