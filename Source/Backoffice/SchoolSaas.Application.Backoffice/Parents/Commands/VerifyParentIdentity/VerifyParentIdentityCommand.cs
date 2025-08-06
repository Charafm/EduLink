using MediatR;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.DataObjects.Parent;

namespace SchoolSaas.Application.Backoffice.Parents.Commands.VerifyParentIdentity
{
    public class VerifyParentIdentityCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public ParentVerificationDTO DTO { get; set; }
    }

    public class VerifyParentIdentityCommandHandler : IRequestHandler<VerifyParentIdentityCommand, bool>
    {
        private readonly IParentService _service;

        public VerifyParentIdentityCommandHandler(IParentService service)
        {
            _service = service;
        }

        public async Task<bool> Handle(VerifyParentIdentityCommand request, CancellationToken cancellationToken)
        {
            return await _service.VerifyParentIdentityAsync(request.Id,request.DTO, cancellationToken);
        }
    }
}