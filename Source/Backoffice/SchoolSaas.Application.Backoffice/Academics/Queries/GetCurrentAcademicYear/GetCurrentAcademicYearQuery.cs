using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Academic;

namespace SchoolSaas.Application.Backoffice.Academics.Queries.GetCurrentAcademicYear{
    public class GetCurrentAcademicYeaQuery : IRequest<AcademicYearDTO>
    {
     
    }

    public class GetCurrentAcademicYeaQueryHandler : IRequestHandler<GetCurrentAcademicYeaQuery, AcademicYearDTO>
    {
        private readonly IAcademicsService _service;

        public GetCurrentAcademicYeaQueryHandler(IAcademicsService service)
        {
            _service = service;
        }

        public async Task<AcademicYearDTO> Handle(GetCurrentAcademicYeaQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetCurrentAcademicYearAsync();
        }
    }
}
