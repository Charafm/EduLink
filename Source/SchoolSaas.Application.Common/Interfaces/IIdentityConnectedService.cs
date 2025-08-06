using SchoolSaas.Application.Common.Models;

namespace SchoolSaas.Application.Common.Interfaces
{
    public interface IIdentityConnectedService
    {
        #region Get By Criteria
        Task<List<PermissionDto>> GetPermissionsByNameAsync(string name );
        Task<List<Guid>> GetUsersByFoPermissionAsync(string permession);
        Task<List<UtilityScopeDto>> GetUtilityScopesByCriteriaAsync(UtilityScopeSearchCriteria criteria );
        Task<List<RoleDto>> GetRoleByCriteria(GetRoleByCriteriaDto criteria );
        #endregion
        #region Update
        Task<Result> UpdateUtilityScope(Guid id, string? title, string? description);
        Task<Result> UpdatePermission(Guid id, string? title, string? description);
        Task<Result> UpdateRole(string id, string? roleName);
        Task<string> ResetPasswordAsync(string UserId);
        #endregion
        #region Create/Add
        Task<Result> CreatePermissionAsync(CreatePermissionDto permissionDto);
        Task<Result> CreateUtilityScopeAsync(CreateUtilityScopeDto utilityScopeDto);
        //Task<User> CreateUser(User data, string password);
        Task<Result> AddRole(string rolename);
        #endregion
        #region Delete
        Task<Result> DeleteRole(string id);
        Task<Result> DeletePermission(Guid id);
        Task<Result> DeleteUtilityScope(Guid id);
        #endregion
        #region Get
        Task<PermissionDto> GetPermissionByIdAsync(Guid id);
        Task<List<PermissionDto>> GetUserPermissionsAsync(string userId );
        Task<List<PermissionDto>> GetRolePermissionsAsync(string roleId );
        Task<List<PermissionDto>> GetAllPermissionsAsync( );
        Task<List<UtilityScopeDto>> GetAllUtilityScopesAsync( );
        Task<UtilityScopeDto> GetUtilityScopeByIdAsync(Guid id);
        Task<List<Guid>> GetUserIdByPost(Guid postId, string Permission);
        Task<List<PermissionDto>> GetUtilityScopePermissionsAsync(Guid utilityScopeId );
        Task<List<UtilityScopeWithPermissionsDto>> GetDefaultUtilityScopesWithStatusAsync(string? roleId);
        Task<List<Guid>> GetUsersByPermissionAsync(string userId);
        Task<AllRolesPermissionsDto> GetAllRolesWithPermissionsAsync();
        #endregion
        #region Assign/Unassign
        Task<Result> AssignPermissionsToUserAsync(AssignPermissionsDto assignPermissionsDto);
        Task<Result> AssignUtilityScopeToRoleAsync(string roleId, Guid utilityScopeId);
        Task ToggleUserStatusAsync(string userId, bool isactive);
        Task<Result> AssignPermissionsToRoleAsync(AssignPermissionsToRoleDto Data);
      
        Task<Result> AssignPermissionsToUtilityScopeAsync(Guid utilityScopeId, List<Guid> permissionIds);
        Task<Result> UnassignPermissionsFromUserAsync(UnassignPermissionsDto unassignPermissionsDto);
        Task<Result> UnassignUtilityScopeFromRoleAsync(string roleId, Guid utilityScopeId);
        Task<Result> UnassignPermissionsFromUtilityScopeAsync(Guid utilityScopeId, List<Guid> permissionIds);

        #endregion
    }
}
