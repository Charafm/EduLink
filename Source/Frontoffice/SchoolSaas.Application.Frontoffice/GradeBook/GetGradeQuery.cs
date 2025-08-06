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
    public class GetGradeQuery : IRequest<PagedResult<GradeDTO>>
    {
        public GradeFilterDTO Filter { get; set; }
    }

    public class GetGradeQueryHandler : IRequestHandler<GetGradeQuery, PagedResult<GradeDTO>>
    {
        private readonly IBackofficeConnectedService _service;

        public GetGradeQueryHandler(IBackofficeConnectedService service) => _service = service;

        public async Task<PagedResult<GradeDTO>> Handle(GetGradeQuery request, CancellationToken cancellationToken)
        {
            return (await _service.GetGrade(request.Filter, cancellationToken)).Data;
        }
    }

}
