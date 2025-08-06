using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using SchoolSaas.Infrastructure.Identity.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolSaas.Infrastructure.Identity.Context;
using SchoolSaas.Infrastructure.Identity.Identity;
using Serilog;

namespace SchoolSaas.Infrastructure.Identity
{
    public class FoPermissionService : IFoPermissionService
    {
        private readonly FrontOfficeIdentityContext _citizenContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        public FoPermissionService(FrontOfficeIdentityContext citizenIdentityContext, ManagerPicker managerPicker)
        {
            _citizenContext = citizenIdentityContext;
            _userManager = managerPicker.GetUserManager(citizenIdentityContext);
            _roleManager = managerPicker.GetRoleManager(citizenIdentityContext);

        }
        #region Citizen Create
        public async Task<bool> CreateCitizenPermissionAsync(CreatePermissionDto permissionDto, CancellationToken cancellationToken)
        {
            if (await _citizenContext.Permissions.AnyAsync(p => p.Name == permissionDto.Name, cancellationToken))
            {
                throw new ApplicationException("Permission already exists!");
            }

            var permission = new Permission
            {
                Name = permissionDto.Name,
                Description = permissionDto.Description,
                Code = permissionDto.Code
            };

            var existingPermission = await _citizenContext.Permissions.FindAsync(permission.Id);

            if (existingPermission != null)
            {
                throw new ApplicationException("Permission already exists!");
            }
            else
            {
                _citizenContext.Permissions.Add(permission);
                await _citizenContext.SaveChangesAsync(cancellationToken);
                return true;
            }
        }

        public async Task<bool> CreateCitizenUtilityScopeAsync(CreateUtilityScopeDto utilityScopeDto, CancellationToken cancellationToken)
        {
            try
            {
                var utilityScope = new UtilityScope
                {
                    Title = utilityScopeDto.Title,
                    Description = utilityScopeDto.Description
                };

                _citizenContext.UtilityScopes.Add(utilityScope);
                await _citizenContext.SaveChangesAsync(cancellationToken);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating utility scope:", ex);
            }
        }
        #endregion

        #region Citizen Delete
        public async Task<bool> DeleteCitizenPermissionAsync(Guid permissionId)
        {
            var existingPermission = await _citizenContext.Permissions.FindAsync(permissionId);
            if (existingPermission == null)
            {
                return false;
            }

            _citizenContext.Permissions.Remove(existingPermission);
            await _citizenContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCitizenUtilityScopeAsync(Guid utilityScopeId)
        {
            var existingUtilityScope = await _citizenContext.UtilityScopes.FindAsync(utilityScopeId);
            if (existingUtilityScope == null)
            {
                return false;
            }

            _citizenContext.UtilityScopes.Remove(existingUtilityScope);
            await _citizenContext.SaveChangesAsync();
            return true;
        }
        #endregion

        #region Citizen Update
        public async Task<bool> UpdateCitizenPermissionAsync(Guid id, string? title, string? description, CancellationToken cancellationToken)
        {
            var permission = await _citizenContext.Permissions.FindAsync(id);
            if (permission == null)
            {
                throw new ApplicationException("Permission not found!");
            }

            if (!string.IsNullOrEmpty(title))
            {
                permission.Name = title;
            }

            if (!string.IsNullOrEmpty(description))
            {
                permission.Description = description;
            }

            _citizenContext.Permissions.Update(permission);
            await _citizenContext.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<bool> UpdateCitizenUtilityScopeAsync(Guid id, string? title, string? description, CancellationToken cancellationToken)
        {
            var utilityScope = await _citizenContext.UtilityScopes.FindAsync(id);
            if (utilityScope == null)
            {
                throw new ApplicationException("Utility scope not found!");
            }

            if (!string.IsNullOrEmpty(title))
            {
                utilityScope.Title = title;
            }

            if (!string.IsNullOrEmpty(description))
            {
                utilityScope.Description = description;
            }

            _citizenContext.UtilityScopes.Update(utilityScope);
            await _citizenContext.SaveChangesAsync(cancellationToken);

            return true;
        }
        #endregion

        #region Citizen Get
        public async Task<List<PermissionDto>> GetAllCitizenPermissionsAsync()
        {
            return await _citizenContext.Permissions
                .Select(p => new PermissionDto { Id = p.Id, Name = p.Name, Description = p.Description, Code = p.Code })
            .ToListAsync();
        }

        public async Task<List<Guid>> GetCitizensWithPermission(string permission)
        {
            var userIdsWithPermission = new List<Guid>();
            var users = await _userManager.Users.ToListAsync();

            foreach (var user in users)
            {
                var permissions = await GetCitizenUserPermissionsAsync(user.Id.ToString());

                if (permissions.Any(p => p.Name == permission))
                {
                    userIdsWithPermission.Add(new Guid(user.Id));
                }
            }

            return userIdsWithPermission;
        }

        public async Task<PermissionDto> GetCitizenPermissionByIdAsync(Guid id)
        {
            var permission = await _citizenContext.Permissions.FindAsync(id);
            if (permission == null)
            {
                throw new ApplicationException("Permission not found!");
            }
            return new PermissionDto { Id = permission.Id, Name = permission.Name, Description = permission.Description, Code = permission.Code };
        }

        public async Task<List<PermissionDto>> GetCitizenUserPermissionsAsync(string userId)
        {
            // Get user-specific permissions
            var userPermissions = await _citizenContext.UserPermissions
                .Where(up => up.UserId == userId)
                .Select(up => new PermissionDto { Id = up.Permission.Id, Name = up.Permission.Name, Description = up.Permission.Description, Code = up.Permission.Code })
                .ToListAsync();

            // Get user details
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new ApplicationException("User not found.");
            }

        
            return userPermissions;
        }

        public async Task<List<PermissionDto>> GetCitizenRolePermissionsAsync(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                throw new ApplicationException("Role does not exist");
            }

            var utilityScopes = await _citizenContext.RoleUtilityScopes
                .Where(r => r.RoleId == role.Id)
                .Select(r => r.UtilityScope)
                .ToListAsync();

            var rolePermissions = new List<PermissionDto>();

            foreach (var scope in utilityScopes)
            {
                var permissions = await _citizenContext.UtilityScopePermissions
                    .Where(usp => usp.UtilityScopeId == scope.Id)
                    .Select(usp => new PermissionDto { Id = usp.Permission.Id, Name = usp.Permission.Name, Description = usp.Permission.Description, Code = usp.Permission.Code })
                    .ToListAsync();

                rolePermissions.AddRange(permissions);
            }

            return rolePermissions;
        }

        public async Task<List<UtilityScopeDto>> GetAllCitizenUtilityScopesAsync()
        {
            var query = _citizenContext.UtilityScopes
                .Select(us => new UtilityScopeDto { Id = us.Id, Title = us.Title, Description = us.Description });

            var items = await query.ToListAsync();
            return items;
        }

        public async Task<UtilityScopeDto> GetCitizenUtilityScopeByIdAsync(Guid id)
        {
            var utilityScope = await _citizenContext.UtilityScopes.FindAsync(id);
            if (utilityScope == null)
            {
                return null;
            }
            return new UtilityScopeDto { Id = utilityScope.Id, Title = utilityScope.Title, Description = utilityScope.Description };
        }

        public async Task<List<PermissionDto>> GetCitizenUtilityScopePermissionsAsync(Guid utilityScopeId)
        {
            var query = _citizenContext.UtilityScopePermissions
                .Where(usp => usp.UtilityScopeId == utilityScopeId)
                .Select(usp => new PermissionDto { Id = usp.Permission.Id, Name = usp.Permission.Name, Description = usp.Permission.Description });

            var items = await query.ToListAsync();
            return items;
        }

        public async Task<List<PermissionDto>> GetCitizenPermissionsByNameAsync(string name)
        {
            var query = _citizenContext.Permissions.AsQueryable().Where(p => p.Name.Contains(name));
            var items = await query
                .Select(p => new PermissionDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Code = p.Code
                })
                .ToListAsync();
            return items;
        }

        public async Task<List<UtilityScopeDto>> GetCitizenUtilityScopesByCriteriaAsync(UtilityScopeSearchCriteria criteria)
        {
            var query = _citizenContext.UtilityScopes
                .Select(us => new UtilityScopeDto
                {
                    Id = us.Id,
                    Title = us.Title,
                    Description = us.Description
                })
                .AsQueryable();

            if (!string.IsNullOrEmpty(criteria.Title))
            {
                query = query.Where(us => us.Title.Contains(criteria.Title));
            }

            if (!string.IsNullOrEmpty(criteria.RoleId))
            {
                query = query.Where(us => _citizenContext.RoleUtilityScopes
                    .Any(rus => rus.UtilityScopeId == us.Id && rus.RoleId == criteria.RoleId));
            }

            if (!string.IsNullOrEmpty(criteria.UserId))
            {
                var user = await _userManager.FindByIdAsync(criteria.UserId);
                if (user != null)
                {
                    var userRoles = await _userManager.GetRolesAsync(user);
                    var userRoleIds = await _citizenContext.Roles
                        .Where(r => userRoles.Contains(r.Name))
                        .Select(r => r.Id)
                        .ToListAsync();

                    if (userRoleIds.Any())
                    {
                        query = query.Where(us => _citizenContext.RoleUtilityScopes
                            .Any(rus => rus.UtilityScopeId == us.Id && userRoleIds.Contains(rus.RoleId)));
                    }
                }
            }

            if (criteria.PermissionId.HasValue)
            {
                query = query.Where(us => _citizenContext.UtilityScopePermissions
                    .Any(usp => usp.UtilityScopeId == us.Id && usp.PermissionId == criteria.PermissionId.Value));
            }

            var items = await query.ToListAsync();
            return items;
        }
        #endregion

        #region Citizen Assigning
        public async Task<bool> AssignCitizenPermissionsToUserAsync(AssignPermissionsDto assignPermissionsDto, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(assignPermissionsDto.UserId);
            if (user == null)
            {
                return false;
            }

            foreach (var permissionId in assignPermissionsDto.PermissionIds)
            {
                if (!await ValidateCitizenPermissionId(permissionId))
                {
                    Log.Error("Permission with the following Id is invalid:", permissionId);
                    continue;
                }

                if (!await _citizenContext.UserPermissions.AnyAsync(up => up.UserId == assignPermissionsDto.UserId && up.PermissionId == permissionId))
                {
                    _citizenContext.UserPermissions.Add(new UserPermission
                    {
                        UserId = assignPermissionsDto.UserId,
                        PermissionId = permissionId
                    });
                }
            }

            await _citizenContext.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<bool> AssignCitizenPermissionsToRoleAsync(string roleId, List<Guid> permissionIds, CancellationToken cancellationToken)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                return false;
            }

            foreach (var permissionId in permissionIds)
            {
                var roleUtilityScope = await _citizenContext.RoleUtilityScopes
                    .FirstOrDefaultAsync(rus => rus.RoleId == roleId && rus.UtilityScopeId == permissionId);

                if (roleUtilityScope == null)
                {
                    return false;
                }

                if (!await _citizenContext.UtilityScopePermissions.AnyAsync(usp => usp.UtilityScopeId == roleUtilityScope.UtilityScopeId && usp.PermissionId == permissionId))
                {
                    _citizenContext.UtilityScopePermissions.Add(new UtilityScopePermission
                    {
                        UtilityScopeId = roleUtilityScope.UtilityScopeId,
                        PermissionId = permissionId
                    });
                }
            }

            await _citizenContext.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<bool> AssignCitizenUtilityScopeToRoleAsync(string roleId, Guid utilityScopeId, CancellationToken cancellationToken)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                throw new ApplicationException("Role invalid!");
            }
            if (!await ValidateCitizenUtilityScopeId(utilityScopeId))
            {
                throw new ApplicationException("Utility Scope invalid!");
            }
            if (!await _citizenContext.RoleUtilityScopes.AnyAsync(rus => rus.RoleId == roleId && rus.UtilityScopeId == utilityScopeId))
            {
                _citizenContext.RoleUtilityScopes.Add(new RoleUtilityScope
                {
                    RoleId = roleId,
                    UtilityScopeId = utilityScopeId
                });

                await _citizenContext.SaveChangesAsync(cancellationToken);
            }

            return true;
        }

        public async Task<bool> AssignCitizenPermissionsToUtilityScopeAsync(Guid utilityScopeId, List<Guid> permissionIds, CancellationToken cancellationToken)
        {
            foreach (var permissionId in permissionIds)
            {
                if (!await _citizenContext.UtilityScopePermissions.AnyAsync(usp => usp.UtilityScopeId == utilityScopeId && usp.PermissionId == permissionId))
                {
                    _citizenContext.UtilityScopePermissions.Add(new UtilityScopePermission
                    {
                        UtilityScopeId = utilityScopeId,
                        PermissionId = permissionId
                    });
                }
            }

            await _citizenContext.SaveChangesAsync(cancellationToken);
            return true;
        }
        #endregion

        #region Citizen Unassigning
        public async Task<bool> UnassignCitizenPermissionsFromUserAsync(UnassignPermissionsDto unassignPermissionsDto, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(unassignPermissionsDto.UserId);
            if (user == null)
            {
                return false;
            }

            foreach (var permissionId in unassignPermissionsDto.PermissionIds)
            {
                var userPermission = await _citizenContext.UserPermissions
                    .FirstOrDefaultAsync(up => up.UserId == unassignPermissionsDto.UserId && up.PermissionId == permissionId);

                if (userPermission != null)
                {
                    _citizenContext.UserPermissions.Remove(userPermission);
                }
            }

            await _citizenContext.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<bool> UnassignCitizenPermissionsFromRoleAsync(string roleId, List<Guid> permissionIds, CancellationToken cancellationToken)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                return false;
            }

            foreach (var permissionId in permissionIds)
            {
                var roleUtilityScope = await _citizenContext.RoleUtilityScopes
                    .FirstOrDefaultAsync(rus => rus.RoleId == roleId && rus.UtilityScopeId == permissionId);

                if (roleUtilityScope != null)
                {
                    var utilityScopePermissions = await _citizenContext.UtilityScopePermissions
                        .Where(usp => usp.UtilityScopeId == roleUtilityScope.UtilityScopeId && usp.PermissionId == permissionId)
                        .ToListAsync();

                    _citizenContext.UtilityScopePermissions.RemoveRange(utilityScopePermissions);
                }
            }

            await _citizenContext.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<bool> UnassignCitizenUtilityScopeFromRoleAsync(string roleId, Guid utilityScopeId, CancellationToken cancellationToken)
        {
            var roleUtilityScope = await _citizenContext.RoleUtilityScopes
                .FirstOrDefaultAsync(rus => rus.RoleId == roleId && rus.UtilityScopeId == utilityScopeId);

            if (roleUtilityScope != null)
            {
                _citizenContext.RoleUtilityScopes.Remove(roleUtilityScope);
                await _citizenContext.SaveChangesAsync(cancellationToken);
            }

            return true;
        }

        public async Task<bool> UnassignCitizenPermissionsFromUtilityScopeAsync(Guid utilityScopeId, List<Guid> permissionIds, CancellationToken cancellationToken)
        {
            foreach (var permissionId in permissionIds)
            {
                var utilityScopePermissions = await _citizenContext.UtilityScopePermissions
                    .Where(usp => usp.UtilityScopeId == utilityScopeId && usp.PermissionId == permissionId)
                    .ToListAsync();

                _citizenContext.UtilityScopePermissions.RemoveRange(utilityScopePermissions);
            }

            await _citizenContext.SaveChangesAsync(cancellationToken);
            return true;
        }
        private async Task<bool> ValidateCitizenUtilityScopeId(Guid utilityScopeId)
        {
            return await _citizenContext.UtilityScopes.AnyAsync(us => us.Id == utilityScopeId);
        }
        private async Task<bool> ValidateCitizenPermissionId(Guid permissionId)
        {
            return await _citizenContext.Permissions.AnyAsync(p => p.Id == permissionId);
        }
        #endregion
    }
}
