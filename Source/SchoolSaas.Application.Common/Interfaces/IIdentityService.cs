using SchoolSaas.Application.Common.Models;

using MediatR;
using SchoolSaas.Domain.Common.DataObjects.Common;

namespace SchoolSaas.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<string> GetUserNameAsync(string userId);
        Task<UserDto> GetUserByIdAsync(string userId);
        Task<PagedResult<UserDto>> GetUsersAsync(int? page, int? size, UserCriteria criteria);
        Task<List<UserDto>> GetUsersByCriteriaAsync(UserCriteria criteria);
        Task<List<UserDto>> GetUsersAsync();
        Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password);
        Task<UserDto> CreateUserAsync(UserDto user, string password = null);
        Task<UserDto> UpdateUserAsync(string userId, UserDto user);
        Task<Result> DeleteUserAsync(string userId);
        Task<Result> ChangePasswordAsync(string userId, string oldPassword, string newPassword);
        Task<string> ResetPasswordAsync(string userId);
        Task<UserDto> GetUserByEmailAsync(string email);
        Task<bool> AnyUserByPhoneAsync(string phone);
        Task<Result> UpdateUserRoleAsync(string userId, string roleName);
        Task<List<UserDto>> GetUserByRolesAsync(List<string> roles);
        Task<Unit> ToggleUserStatusAsync(string userId, bool isActive);
        Task<List<string>> GetUserClaimesAsync(string userId, string claimType);
        Task<List<RoleDto>> GetRolesAsync();
        Task<List<RoleDto>> GetRolesByCriteriaAsync(GetRoleByCriteriaDto criteria);
        Task<bool> CreateRoleAsync(string roleName);
        Task<bool> UpdateRoleAsync(string roleId, string newRoleName);
        Task<bool> DeleteRoleAsync(string roleId);
        Task<List<string>> GetRoleClaimsAsync(string roleId, string claimType);
        Task<bool> VerifyUserToken(string token, string userId);
        Task<string> GetUserToken(string userId);
    }
}