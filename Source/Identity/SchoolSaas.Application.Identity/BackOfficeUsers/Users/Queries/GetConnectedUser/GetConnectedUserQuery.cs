//using SchoolSaas.Application.Common.Exceptions;
//using SchoolSaas.Application.Common.Interfaces;
//using SchoolSaas.Domain.Constants;
//using SchoolSaas.Domain.Common.DataObjects;

//using MediatR;

//namespace SchoolSaas.Application.Identity.BackOfficeUsers.Users.Queries.GetConnectedUser
//{
//    public class GetConnectedUserQuery : IRequest<User>
//    {
//    }

//    public class GetConnectedUserQueryHandler : IRequestHandler<GetConnectedUserQuery, User>
//    {
//        private readonly ICurrentUserService _currentUserService;
//        private readonly IIdentityService _identityService;

//        public GetConnectedUserQueryHandler(IIdentityService identityService, ICurrentUserService currentUserService)
//        {
//            _identityService = identityService;
//            _currentUserService = currentUserService;
//        }

//        public async Task<User> Handle(GetConnectedUserQuery request, CancellationToken cancellationToken)
//        {
//            var entity = await _identityService.GetUserByIdAsync(_currentUserService.UserId);

//            NotFoundException.ThrowIfNull(nameof(User), _currentUserService.UserId);

//            entity.Permissions = await _identityService.GetUserClaimesAsync(entity.Id, AuthorizationConstants.ClaimTypes.Permissions);

//            return entity;
//        }
//    }
//}