using SchoolSaas.Application.Common.Models;

namespace SchoolSaas.Application.Common.Interfaces
{
    public interface IFoPermissionService
    {
        #region Citizen Create
        Task<bool> CreateCitizenPermissionAsync(CreatePermissionDto permissionDto, CancellationToken cancellationToken);
        Task<bool> CreateCitizenUtilityScopeAsync(CreateUtilityScopeDto utilityScopeDto, CancellationToken cancellationToken);
        #endregion

        #region Citizen Delete
        Task<bool> DeleteCitizenPermissionAsync(Guid permissionId);
        Task<bool> DeleteCitizenUtilityScopeAsync(Guid utilityScopeId);
        #endregion

        #region Citizen Update
        Task<bool> UpdateCitizenPermissionAsync(Guid id, string? title, string? description, CancellationToken cancellationToken);
        Task<bool> UpdateCitizenUtilityScopeAsync(Guid id, string? title, string? description, CancellationToken cancellationToken);
        #endregion

        #region Citizen Get
        Task<List<PermissionDto>> GetAllCitizenPermissionsAsync();
        Task<List<Guid>> GetCitizensWithPermission(string permission);
        Task<PermissionDto> GetCitizenPermissionByIdAsync(Guid id);
        Task<List<PermissionDto>> GetCitizenUserPermissionsAsync(string userId);
        Task<List<PermissionDto>> GetCitizenRolePermissionsAsync(string roleId);
        Task<List<UtilityScopeDto>> GetAllCitizenUtilityScopesAsync();
        Task<UtilityScopeDto> GetCitizenUtilityScopeByIdAsync(Guid id);
        Task<List<PermissionDto>> GetCitizenUtilityScopePermissionsAsync(Guid utilityScopeId);
        Task<List<PermissionDto>> GetCitizenPermissionsByNameAsync(string name);
        Task<List<UtilityScopeDto>> GetCitizenUtilityScopesByCriteriaAsync(UtilityScopeSearchCriteria criteria);
        #endregion

        #region Citizen Assigning
        Task<bool> AssignCitizenPermissionsToUserAsync(AssignPermissionsDto assignPermissionsDto, CancellationToken cancellationToken);
        Task<bool> AssignCitizenPermissionsToRoleAsync(string roleId, List<Guid> permissionIds, CancellationToken cancellationToken);
        Task<bool> AssignCitizenUtilityScopeToRoleAsync(string roleId, Guid utilityScopeId, CancellationToken cancellationToken);
        Task<bool> AssignCitizenPermissionsToUtilityScopeAsync(Guid utilityScopeId, List<Guid> permissionIds, CancellationToken cancellationToken);
        #endregion

        #region Citizen Unassigning
        Task<bool> UnassignCitizenPermissionsFromUserAsync(UnassignPermissionsDto unassignPermissionsDto, CancellationToken cancellationToken);
        Task<bool> UnassignCitizenPermissionsFromRoleAsync(string roleId, List<Guid> permissionIds, CancellationToken cancellationToken);
        Task<bool> UnassignCitizenUtilityScopeFromRoleAsync(string roleId, Guid utilityScopeId, CancellationToken cancellationToken);
        Task<bool> UnassignCitizenPermissionsFromUtilityScopeAsync(Guid utilityScopeId, List<Guid> permissionIds, CancellationToken cancellationToken);
        #endregion
    }
}
