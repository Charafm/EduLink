using SchoolSaas.Domain.Common.Localization;
using MediatR.Behaviors.Authorization;
using Microsoft.Extensions.Localization;
using SchoolSaas.Application.Common.Interfaces;

namespace SchoolSaas.Application.Common.Authorizations
{
    public class MustHaveRoleRequirement : IAuthorizationRequirement
    {
        public List<string> RoleNames { get; set; }

        private class MustHaveRoleRequirementHandler : IAuthorizationHandler<MustHaveRoleRequirement>
        {
            private readonly ICurrentUserService _currentUserService;
            private readonly IStringLocalizer<Messages> _localizer;

            public MustHaveRoleRequirementHandler(ICurrentUserService currentUserService, IStringLocalizer<Messages> localizer)
            {
                _currentUserService = currentUserService;
                _localizer = localizer;
            }

            public Task<AuthorizationResult> Handle(MustHaveRoleRequirement request,
                CancellationToken cancellationToken)
            {
                if (_currentUserService.RoleNames != null && _currentUserService.RoleNames.Intersect(request.RoleNames).Any())
                {
                    return Task.FromResult(AuthorizationResult.Succeed());
                }

                return Task.FromResult(AuthorizationResult.Fail(_localizer["PermissionDenied"]));
            }
        }
    }
}