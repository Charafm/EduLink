using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Grade;

namespace SchoolSaas.Application.Backoffice.Grades.Commands.BulkUpdateGrades
{
    public class BulkUpdateGradesCommand : IRequest<bool>
    {
        public BulkGradeUpdateDTO DTO { get; set; }
    }

    public class BulkUpdateGradesCommandHandler : IRequestHandler<BulkUpdateGradesCommand, bool>
    {
        private readonly IGradebookService _service;

        public BulkUpdateGradesCommandHandler(IGradebookService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(BulkUpdateGradesCommand request, CancellationToken cancellationToken)
        {
            return await _service.BulkUpdateGradesAsync(request.DTO, cancellationToken);
        }
    }
    
}