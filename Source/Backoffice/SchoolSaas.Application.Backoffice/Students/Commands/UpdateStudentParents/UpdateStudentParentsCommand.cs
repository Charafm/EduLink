using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Parent;

namespace SchoolSaas.Application.Backoffice.Students.Commands.UpdateStudentParents
{
    public class UpdateStudentParentsCommand : IRequest<bool>
    {
        public Guid StudentId { get; set; }
        public List<ParentLinkDTO> ParentLinks { get; set; }
    }
    public class UpdateStudentParentsCommandHandler : IRequestHandler<UpdateStudentParentsCommand, bool>
    {
        private readonly IStudentService _studentService;

        public UpdateStudentParentsCommandHandler(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public async Task<bool> Handle(UpdateStudentParentsCommand request, CancellationToken cancellationToken)
        {
            return await _studentService.UpdateStudentParentsAsync(request.StudentId, request.ParentLinks, cancellationToken);
        }
    }
}
