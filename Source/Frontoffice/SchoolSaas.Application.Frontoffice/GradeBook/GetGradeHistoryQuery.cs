using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Common.DataObjects.Grade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Application.Frontoffice.GradeBook
{
    public class GetGradeHistoryQuery : IRequest<PagedResult<GradeHistoryDTO>>
    {
        public GradeHistoryFilterDTO Filter { get; set; }
    }

    public class GetGradeHistoryQueryHandler : IRequestHandler<GetGradeHistoryQuery, PagedResult<GradeHistoryDTO>>
    {
        private readonly IBackofficeConnectedService _service;

        public GetGradeHistoryQueryHandler(IBackofficeConnectedService service) => _service = service;

        public async Task<PagedResult<GradeHistoryDTO>> Handle(GetGradeHistoryQuery request, CancellationToken cancellationToken)
        {
            return (await _service.GetGradeHistory(request.Filter, cancellationToken)).Data;
        }
    }

}
