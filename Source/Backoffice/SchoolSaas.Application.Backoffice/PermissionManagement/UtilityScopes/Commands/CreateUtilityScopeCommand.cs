using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using MediatR;

namespace SchoolSaas.Application.Backoffice.PermissionManagement.UtilityScopes.Commands
{
    public class CreateUtilityScopeCommand : IRequest<Result>
    {
        public CreateUtilityScopeDto UtilityScopeDto { get; set; }
    }
    public class CreateUtilityScopeCommandHandler : IRequestHandler<CreateUtilityScopeCommand, Result>
    {
        private readonly IIdentityConnectedService _service;

        public CreateUtilityScopeCommandHandler(IIdentityConnectedService service)
        {
            _service = service;
        }

        public async Task<Result> Handle(CreateUtilityScopeCommand request, CancellationToken cancellationToken)
        {
            return await _service.CreateUtilityScopeAsync(request.UtilityScopeDto);
        }
    }
}
