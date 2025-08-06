using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using MediatR;

namespace SchoolSaas.Application.Backoffice.PermissionManagement.UtilityScopes.Commands
{
   
    public class DeleteUtilityScopeCommand : IRequest<Result>
    {
        public Guid id { get; set; }
    }
    public class DeletePemrissionCommandHandler : IRequestHandler<DeleteUtilityScopeCommand, Result>
    {
        private readonly IIdentityConnectedService _service;

        public DeletePemrissionCommandHandler(IIdentityConnectedService service)
        {
            _service = service;
        }

        public async Task<Result> Handle(DeleteUtilityScopeCommand request, CancellationToken cancellationToken)
        {
            return await _service.DeleteUtilityScope(request.id);
        }
    }
}
