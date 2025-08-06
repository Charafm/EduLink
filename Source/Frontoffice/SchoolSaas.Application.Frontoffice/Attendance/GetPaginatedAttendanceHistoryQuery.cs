using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using SchoolSaas.Domain.Common.DataObjects.Attendance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Application.Frontoffice.Attendance
{
    public class GetPaginatedAttendanceHistoryQuery : IRequest<ResponseDto<PagedResult<AttendanceHistoryDTO>>>
    {
        public AttendanceHistoryFilterDTO Filter { get; set; }
    }

    public class GetPaginatedAttendanceHistoryQueryHandler : IRequestHandler<GetPaginatedAttendanceHistoryQuery, ResponseDto<PagedResult<AttendanceHistoryDTO>>>
    {
        private readonly IBackofficeConnectedService _service;
        public GetPaginatedAttendanceHistoryQueryHandler(IBackofficeConnectedService service) => _service = service;

        public async Task<ResponseDto<PagedResult<AttendanceHistoryDTO>>> Handle(GetPaginatedAttendanceHistoryQuery request, CancellationToken cancellationToken)
            => await _service.GetPaginatedAttendanceHistory(request.Filter, cancellationToken);
    }
}
