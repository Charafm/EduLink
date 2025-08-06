using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Grade;

namespace SchoolSaas.Application.Backoffice.Grades.Commands.RecordGrade
{
    public class RecordGradeCommand : IRequest<bool>
    {
        public GradeDTO DTO { get; set; }
    }

    public class RecordGradeCommandHandler : IRequestHandler<RecordGradeCommand, bool>
    {
        private readonly IGradebookService _service;

        public RecordGradeCommandHandler(IGradebookService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(RecordGradeCommand request, CancellationToken cancellationToken)
        {
            return await _service.RecordGradeAsync(request.DTO,cancellationToken);
        }
    }

}