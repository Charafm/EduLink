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

    public class GetAttendanceRecordsQuery : IRequest<ResponseDto<ICollection<AttendanceDTO>>>
    {
        public Guid StudentId { get; set; }
    }

    public class GetAttendanceRecordsQueryHandler : IRequestHandler<GetAttendanceRecordsQuery, ResponseDto<ICollection<AttendanceDTO>>>
    {
        private readonly IBackofficeConnectedService _service;
        public GetAttendanceRecordsQueryHandler(IBackofficeConnectedService service) => _service = service;

        public async Task<ResponseDto<ICollection<AttendanceDTO>>> Handle(GetAttendanceRecordsQuery request, CancellationToken cancellationToken)
            => await _service.GetAttendanceRecords(request.StudentId, cancellationToken);
    }

}
