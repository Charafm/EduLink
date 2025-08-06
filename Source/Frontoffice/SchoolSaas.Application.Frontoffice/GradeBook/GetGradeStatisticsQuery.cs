using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Grade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Application.Frontoffice.GradeBook
{
    public class GetGradeStatisticsQuery : IRequest<GradeStatisticsDTO>
    {
        public GradeFilterDTO Filter { get; set; }
    }

    public class GetGradeStatisticsQueryHandler : IRequestHandler<GetGradeStatisticsQuery, GradeStatisticsDTO>
    {
        private readonly IBackofficeConnectedService _service;

        public GetGradeStatisticsQueryHandler(IBackofficeConnectedService service) => _service = service;

        public async Task<GradeStatisticsDTO> Handle(GetGradeStatisticsQuery request, CancellationToken cancellationToken)
        {
            return (await _service.GetGradeStatistics(request.Filter, cancellationToken)).Data;
        }
    }

}
