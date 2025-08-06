using MediatR.Behaviors.Authorization;
using Microsoft.Extensions.Localization;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.Localization;

namespace SchoolSaas.Application.Common.Authorizations
{
    public class MustHavePermissionRequirement : IAuthorizationRequirement
    {
        public List<string> Permissions { get; set; } = new List<string>();

        private class MustHavePermissionRequirementHandler : IAuthorizationHandler<MustHavePermissionRequirement>
        {
            private readonly ICurrentUserService _currentUserService;
            private readonly IStringLocalizer<Messages> _localizer;

            public MustHavePermissionRequirementHandler(ICurrentUserService currentUserService, IStringLocalizer<Messages> localizer)
            {
                _currentUserService = currentUserService;
                _localizer = localizer;
            }

            public Task<AuthorizationResult> Handle(MustHavePermissionRequirement request,
                CancellationToken cancellationToken)
            {
                return Task.FromResult(AuthorizationResult.Fail(_localizer["PermissionDenied"]));
            }
        }
    }
}