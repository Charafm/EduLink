using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Grade;

namespace SchoolSaas.Application.Backoffice.Grades.Commands.BulkRecordGrades
{
    public class BulkRecordGradesCommand : IRequest<bool>
    {
        public BulkGradeDTO DTO { get; set; }

    }

    public class BulkRecordGradesCommandHandler : IRequestHandler<BulkRecordGradesCommand, bool>
    {
        private readonly IGradebookService _service;

        public BulkRecordGradesCommandHandler(IGradebookService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(BulkRecordGradesCommand request, CancellationToken cancellationToken)
        {
            return await _service.BulkRecordGradesAsync(request.DTO, cancellationToken);
        }
    }

}