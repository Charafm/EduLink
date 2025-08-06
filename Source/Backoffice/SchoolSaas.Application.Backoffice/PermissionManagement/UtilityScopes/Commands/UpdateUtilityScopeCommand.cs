using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using MediatR;

namespace SchoolSaas.Application.Backoffice.PermissionManagement.UtilityScopes.Commands
{
    public class UpdateUtilityScopeCommand : IRequest<Result>
    {
        public Guid id { get; set; }
        public string? name { get; set; }
        public string? description { get; set; }
    }


    public class UpdateUtilityScopeCommandHandler : IRequestHandler<UpdateUtilityScopeCommand, Result>
    {
        private readonly IIdentityConnectedService _service;

        public UpdateUtilityScopeCommandHandler(IIdentityConnectedService service)
        {
            _service = service;
        }

        public async Task<Result> Handle(UpdateUtilityScopeCommand request, CancellationToken cancellationToken)
        {
            return await _service.UpdateUtilityScope(request.id, request.name, request.description);
        }
    }
}
