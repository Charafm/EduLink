//using SchoolSaas.Application.Common.Exceptions;
//using SchoolSaas.Application.Common.Interfaces;
//using SchoolSaas.Domain.Constants;
//using SchoolSaas.Domain.Common.DataObjects;

//using MediatR;

//namespace SchoolSaas.Application.Identity.FrontOfficeUsers.Users.Queries.GetConnectedUser
//{
//    public class GetConnectedFoUserQuery : IRequest<User>
//    {
//    }

//    public class GetConnectedUserQueryHandler : IRequestHandler<GetConnectedFoUserQuery, User>
//    {
//        private readonly ICurrentUserService _currentUserService;
//        private readonly IStaffIdentityService _identityService;

//        public GetConnectedUserQueryHandler(IStaffIdentityService identityService, ICurrentUserService currentUserService)
//        {
//            _identityService = identityService;
//            _currentUserService = currentUserService;
//        }

//        public async Task<User> Handle(GetConnectedFoUserQuery request, CancellationToken cancellationToken)
//        {
//            var entity = await _identityService.GetUserByIdAsync(_currentUserService.UserId);

//            NotFoundException.ThrowIfNull(nameof(User), _currentUserService.UserId);

//            entity.Permissions = await _identityService.GetUserClaimesAsync(entity.Id, AuthorizationConstants.ClaimTypes.Permissions);

//            return entity;
//        }
//    }
//}