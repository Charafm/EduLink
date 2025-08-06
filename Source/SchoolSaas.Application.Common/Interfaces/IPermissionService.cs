using SchoolSaas.Application.Common.Models;

namespace SchoolSaas.Application.Common.Interfaces
{
    using System.Threading;

    public interface IPermissionService
    {
        #region Create
        Task<bool> CreatePermissionAsync(CreatePermissionDto permissionDto, CancellationToken cancellationToken);
        Task<bool> CreateUtilityScopeAsync(CreateUtilityScopeDto utilityScopeDto, CancellationToken cancellationToken);
        #endregion
        #region Delete
        Task<bool> DeletePermissionAsync(Guid permissionId);
        Task<bool> DeleteUtilityScopeAsync(Guid utilityScopeId);
        #endregion
        #region Update
        Task<bool> UpdatePermissionAsync(Guid id, string? title, string? description, CancellationToken cancellationToken);
        Task<bool> UpdateUtilityScopeAsync(Guid id, string? title, string? description, CancellationToken cancellationToken);
        #endregion

        #region Get
        Task<List<PermissionDto>> GetAllPermissionsAsync();
        Task<List<Guid>> GetUsersWithPermission(string permission);
        Task<PermissionDto> GetPermissionByIdAsync(Guid id);
        Task<List<UtilityScopeWithPermissionsDto>> GetDefaultUtilityScopesWithStatusAsync(string? roleId, CancellationToken cancellationToken);
        Task<List<PermissionDto>> GetUserPermissionsAsync(string userId);
        Task<List<PermissionDto>> GetRolePermissionsAsync(string roleId);
        Task<List<UtilityScopeDto>> GetAllUtilityScopesAsync();
        Task<UtilityScopeDto> GetUtilityScopeByIdAsync(Guid id);
        Task<List<PermissionDto>> GetUtilityScopePermissionsAsync(Guid utilityScopeId);
        Task<List<PermissionDto>> GetPermissionsByNameAsync(string name);
        Task<List<UtilityScopeDto>> GetUtilityScopesByCriteriaAsync(UtilityScopeSearchCriteria criteria);
        #endregion

        #region Assigning
        Task<bool> AssignPermissionsToUserAsync(AssignPermissionsDto assignPermissionsDto, CancellationToken cancellationToken );
        Task<bool> AssignPermissionsToRoleAsync(string roleId, List<Guid> permissionIds, CancellationToken cancellationToken );
        Task<bool> AssignUtilityScopeToRoleAsync(string roleId, Guid utilityScopeId, CancellationToken cancellationToken);
        Task<bool> AssignPermissionsToUtilityScopeAsync(Guid utilityScopeId, List<Guid> permissionIds, CancellationToken cancellationToken);
        #endregion

        #region Unassigning
        Task<bool> UnassignPermissionsFromUserAsync(UnassignPermissionsDto unassignPermissionsDto, CancellationToken cancellationToken);
        Task<bool> UnassignPermissionsFromRoleAsync(string roleId, List<Guid> permissionIds, CancellationToken cancellationToken);
        Task<bool> UnassignUtilityScopeFromRoleAsync(string roleId, Guid utilityScopeId, CancellationToken cancellationToken);
        Task<bool> UnassignPermissionsFromUtilityScopeAsync(Guid utilityScopeId, List<Guid> permissionIds, CancellationToken cancellationToken);
        #endregion
        
    }


}
