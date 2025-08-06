using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolSaas.Infrastructure.Identity.Identity.Entities;
using Serilog;

using SchoolSaas.Infrastructure.Identity.Identity;
using SchoolSaas.Infrastructure.Identity.Context;

namespace SchoolSaas.Infrastructure.Identity
{
    public class PermissionService : IPermissionService
    {
        private readonly IdentityContext _context;

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public PermissionService(IdentityContext context, ManagerPicker managerPicker)
        {
            _context = context;
            _userManager = managerPicker.GetUserManager(context);
            _roleManager = managerPicker.GetRoleManager(context);

        }

        #region Permissions
        public async Task<List<PermissionDto>> GetAllPermissionsAsync()
        {
            return await _context.Permissions
         .Select(p => new PermissionDto { Id = p.Id, Name = p.Name, Description = p.Description, Code = p.Code })
         .ToListAsync();
        }


        public async Task<PermissionDto> GetPermissionByIdAsync(Guid id)
        {
            var permission = await _context.Permissions.FindAsync(id);
            if (permission == null)
            {
                throw new ApplicationException("Permission not found!");
            }
            return new PermissionDto { Id = permission.Id, Name = permission.Name, Description = permission.Description, Code = permission.Code };
        }

        public async Task<List<PermissionDto>> GetPermissionsByNameAsync(string name)
        {
            var query = _context.Permissions.AsQueryable().Where(p => p.Name.Contains(name));
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


        public async Task<List<UtilityScopeWithPermissionsDto>> GetDefaultUtilityScopesWithStatusAsync(string? roleId, CancellationToken cancellationToken)
        {
            // Retrieve the utility scopes assigned to the role
            var assignedUtilityScopeIds = await _context.RoleUtilityScopes
                .Where(rus => rus.RoleId == roleId)
                .Select(rus => rus.UtilityScopeId)
                .ToListAsync(cancellationToken);

            // Retrieve the permissions assigned to these utility scopes
            var assignedPermissionIds = await _context.UtilityScopePermissions
                .Where(usp => assignedUtilityScopeIds.Contains(usp.UtilityScopeId))
                .Select(usp => usp.PermissionId)
                .ToListAsync(cancellationToken);

            // Retrieve all default utility scopes and their permissions
            var defaultUtilityScopes = await _context.UtilityScopes
                .Include(us => us.UtilityScopePermissions)
                    .ThenInclude(usp => usp.Permission)
                .Where(us => !us.Description.Contains("Custom"))
                .ToListAsync(cancellationToken);

            // Map the results to DTOs with active status based on assigned permissions
            var result = defaultUtilityScopes.Select(us => new UtilityScopeWithPermissionsDto
            {
                UtilityScopeId = us.Id,
                Title = us.Title,
                Description = us.Description,
                Permissions = us.UtilityScopePermissions
                    .Where(usp => usp.Permission != null)
                    .Select(usp => new PermissionStatusDto
                    {
                        permission = new PermissionDto
                        {
                            Id = usp.Permission.Id,
                            Name = usp.Permission.Name,
                            Description = usp.Permission.Description,
                            Code = usp.Permission.Code
                        },
                        IsActive = assignedPermissionIds.Contains(usp.PermissionId)
                    }).ToList()
            }).ToList();

            return result;
        }


        public async Task<bool> CreatePermissionAsync(CreatePermissionDto permissionDto, CancellationToken cancellationToken)
        {
            if (await _context.Permissions.AnyAsync(p => p.Name == permissionDto.Name, cancellationToken))
            {
                throw new ApplicationException("Permission already exists!");
            }

            var permission = new Permission
            {
                Name = permissionDto.Name,
                Description = permissionDto.Description,
                Code = permissionDto.Code
            };

            var existingPermission = await _context.Permissions.FindAsync(permission.Id);

            if (existingPermission != null)
            {
                throw new ApplicationException("Permission already exists!");
            }
            else
            {
                _context.Permissions.Add(permission);
                await _context.SaveChangesAsync(cancellationToken);

                return true;
            }
        }


        private async Task<bool> ValidatePermissionId(Guid id)
        {
            return await _context.Permissions.AnyAsync(p => p.Id == id);
        }

        public async Task<bool> DeleteUtilityScopeAsync(Guid utilityScopeId)
        {
            var existingUtilityScope = await _context.UtilityScopes.FindAsync(utilityScopeId);

            if (existingUtilityScope == null)
            {
                return false;
            }

            _context.UtilityScopes.Remove(existingUtilityScope);
            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<bool> DeletePermissionAsync(Guid permissionId)
        {
            var existingPermission = await _context.Permissions.FindAsync(permissionId);

            if (existingPermission == null)
            {
                return false;
            }

            _context.Permissions.Remove(existingPermission);
            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<bool> AssignPermissionsToUserAsync(AssignPermissionsDto assignPermissionsDto, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(assignPermissionsDto.UserId);
            if (user == null)
            {
                return false;
            }

            // Retrieve the current permissions assigned to the user
            var currentPermissions = await _context.UserPermissions
                .Where(up => up.UserId == assignPermissionsDto.UserId)
                .ToListAsync(cancellationToken);

            var currentPermissionIds = currentPermissions.Select(up => up.PermissionId).ToHashSet();

            // Identify permissions to remove and to add
            var permissionsToRemove = currentPermissions
                .Where(up => !assignPermissionsDto.PermissionIds.Contains(up.PermissionId))
                .ToList();

            var permissionsToAdd = assignPermissionsDto.PermissionIds
                .Where(pid => !currentPermissionIds.Contains(pid))
                .ToList();

            // Remove old permissions
            _context.UserPermissions.RemoveRange(permissionsToRemove);

            // Add new permissions
            foreach (var permissionId in permissionsToAdd)
            {
                if (await ValidatePermissionId(permissionId))
                {
                    _context.UserPermissions.Add(new UserPermission
                    {
                        UserId = assignPermissionsDto.UserId,
                        PermissionId = permissionId
                    });
                }
                else
                {
                    Log.Error("Permission with the following Id is invalid: {PermissionId}", permissionId);
                }
            }

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }


        public async Task<bool> AssignPermissionsToRoleAsync(string roleId, List<Guid> permissionIds, CancellationToken cancellationToken)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                return false;
            }

            // Retrieve all default utility scopes and their permissions
            var defaultUtilityScopes = await _context.UtilityScopes
                .Include(us => us.UtilityScopePermissions)
                .Where(us => !us.Description.Contains("Custom"))
                .ToListAsync(cancellationToken);

            var matchedScopes = new List<UtilityScope>();
            var remainingPermissions = new HashSet<Guid>(permissionIds);

            // Identify fully matching and partially matching utility scopes
            foreach (var scope in defaultUtilityScopes)
            {
                var scopePermissionIds = scope.UtilityScopePermissions.Select(usp => usp.PermissionId).ToList();

                if (scopePermissionIds.All(permissionIds.Contains))
                {
                    matchedScopes.Add(scope);
                    foreach (var permissionId in scopePermissionIds)
                    {
                        remainingPermissions.Remove(permissionId);
                    }
                }
            }

            // Create a dictionary to keep track of remaining permissions and their corresponding default scopes
            var remainingPermissionsByScope = new Dictionary<UtilityScope, HashSet<Guid>>();

            // Distribute remaining permissions among their default utility scopes
            foreach (var permissionId in remainingPermissions.ToList())
            {
                foreach (var scope in defaultUtilityScopes)
                {
                    if (scope.UtilityScopePermissions.Any(usp => usp.PermissionId == permissionId))
                    {
                        if (!remainingPermissionsByScope.ContainsKey(scope))
                        {
                            remainingPermissionsByScope[scope] = new HashSet<Guid>();
                        }
                        remainingPermissionsByScope[scope].Add(permissionId);
                        remainingPermissions.Remove(permissionId);
                        break;
                    }
                }
            }

            // Check for existing custom utility scopes
            foreach (var permissionId in remainingPermissions.ToList())
            {
                var existingCustomScope = await _context.UtilityScopes
                    .Include(us => us.UtilityScopePermissions)
                    .FirstOrDefaultAsync(us => us.UtilityScopePermissions.Any(usp => usp.PermissionId == permissionId)
                        && !us.Description.Contains("default", StringComparison.OrdinalIgnoreCase), cancellationToken);

                if (existingCustomScope != null)
                {
                    matchedScopes.Add(existingCustomScope);
                    foreach (var perm in existingCustomScope.UtilityScopePermissions)
                    {
                        remainingPermissions.Remove(perm.PermissionId);
                    }
                }
            }

            // Create new utility scopes for remaining permissions based on their default utility scopes
            foreach (var kvp in remainingPermissionsByScope)
            {
                var defaultScope = kvp.Key;
                var permissions = kvp.Value;

                var newUtilityScope = new UtilityScope
                {
                    Title = defaultScope.Title,
                    Description = $"{defaultScope.Title} - Custom scope for role: {role.Name} - {DateTime.UtcNow:yyyy-MM-dd}"
                };
                _context.UtilityScopes.Add(newUtilityScope);
                await _context.SaveChangesAsync(cancellationToken);

                foreach (var permissionId in permissions)
                {
                    _context.UtilityScopePermissions.Add(new UtilityScopePermission
                    {
                        UtilityScopeId = newUtilityScope.Id,
                        PermissionId = permissionId
                    });
                }
                await _context.SaveChangesAsync(cancellationToken);

                matchedScopes.Add(newUtilityScope);
            }

            // Unassign the previous utility scopes from the role
            var previousRoleUtilityScopes = await _context.RoleUtilityScopes
                .Where(rus => rus.RoleId == roleId)
                .ToListAsync(cancellationToken);

            _context.RoleUtilityScopes.RemoveRange(previousRoleUtilityScopes);
            await _context.SaveChangesAsync(cancellationToken);

            // Assign the new or matched utility scopes to the role
            foreach (var matchedScope in matchedScopes)
            {
                _context.RoleUtilityScopes.Add(new RoleUtilityScope
                {
                    RoleId = roleId,
                    UtilityScopeId = matchedScope.Id
                });
            }
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }



        public async Task<List<Guid>> GetUsersWithPermission(string permission)
        {
            var userIdsWithPermission = new List<Guid>();
            var users = await _userManager.Users.ToListAsync();

            foreach (var user in users)
            {
                var permissions = await GetUserPermissionsAsync(user.Id.ToString());

                if (permissions.Any(p => p.Name == permission))
                {
                    userIdsWithPermission.Add(new Guid(user.Id));
                }
            }

            return userIdsWithPermission;
        }
        public async Task<List<PermissionDto>> GetUserPermissionsAsync(string userId)
        {
            // Check user in IdentityContext
            var user =  _context.Set<ApplicationUser>().IgnoreQueryFilters()
               .FirstOrDefault(u => u.Id == userId);



            if (user == null)
            {
                throw new ApplicationException("User not found in any identity context.");
            }

            // Get user-specific permissions
            var userPermissions = await _context.UserPermissions
                .Where(up => up.UserId == userId)
                .Select(up => new PermissionDto
                {
                    Id = up.Permission.Id,
                    Name = up.Permission.Name,
                    Description = up.Permission.Description,
                    Code = up.Permission.Code
                })
                .ToListAsync();

            return userPermissions;
        }




        public async Task<List<PermissionDto>> GetRolePermissionsAsync(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                throw new ApplicationException("Role does not exist");
            }

            var utilityScopes = await _context.RoleUtilityScopes
                .Where(r => r.RoleId == role.Id)
                .Select(r => r.UtilityScope)
                .ToListAsync();

            var rolePermissions = new List<PermissionDto>();

            foreach (var scope in utilityScopes)
            {
                var permissions = await _context.UtilityScopePermissions
                    .Where(usp => usp.UtilityScopeId == scope.Id)
                    .Select(usp => new PermissionDto { Id = usp.Permission.Id, Name = usp.Permission.Name, Description = usp.Permission.Description, Code = usp.Permission.Code })
                    .ToListAsync();

                rolePermissions.AddRange(permissions);
            }

            return rolePermissions;
        }


        public async Task<bool> UnassignPermissionsFromRoleAsync(string roleId, List<Guid> permissionIds, CancellationToken cancellationToken)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                return false;
            }

            foreach (var permissionId in permissionIds)
            {

                var roleUtilityScope = await _context.RoleUtilityScopes
                    .FirstOrDefaultAsync(rus => rus.RoleId == roleId && rus.UtilityScopeId == permissionId);

                if (roleUtilityScope != null)
                {
                    var utilityScopePermissions = await _context.UtilityScopePermissions
                        .Where(usp => usp.UtilityScopeId == roleUtilityScope.UtilityScopeId && usp.PermissionId == permissionId)
                        .ToListAsync();

                    _context.UtilityScopePermissions.RemoveRange(utilityScopePermissions);
                }
            }

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
        public async Task<bool> UnassignPermissionsFromUserAsync(UnassignPermissionsDto unassignPermissionsDto, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(unassignPermissionsDto.UserId);
            if (user == null)
            {
                return false;
            }

            foreach (var permissionId in unassignPermissionsDto.PermissionIds)
            {

                var userPermission = await _context.UserPermissions
                    .FirstOrDefaultAsync(up => up.UserId == unassignPermissionsDto.UserId && up.PermissionId == permissionId);

                if (userPermission != null)
                {
                    _context.UserPermissions.Remove(userPermission);
                }
            }

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
        public async Task<bool> UpdatePermissionAsync(Guid id, string? title, string? description, CancellationToken cancellationToken)
        {
            var permission = await _context.Permissions.FindAsync(id);
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

            _context.Permissions.Update(permission);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
        #endregion

        #region UtilityScope
        public async Task<List<PermissionDto>> GetUtilityScopePermissionsAsync(Guid utilityScopeId)
        {

            var query = _context.UtilityScopePermissions
                .Where(usp => usp.UtilityScopeId == utilityScopeId)
                .Select(usp => new PermissionDto { Id = usp.Permission.Id, Name = usp.Permission.Name, Description = usp.Permission.Description, Code = usp.Permission.Code });

            var items = await query.ToListAsync();

            return items;
        }


        public async Task<List<UtilityScopeDto>> GetAllUtilityScopesAsync()
        {
            var query = _context.UtilityScopes
                .Select(us => new UtilityScopeDto { Id = us.Id, Title = us.Title, Description = us.Description });

            var items = await query
                .ToListAsync();

            return items;
        }


        public async Task<UtilityScopeDto> GetUtilityScopeByIdAsync(Guid id)
        {

            var utilityScope = await _context.UtilityScopes.FindAsync(id);
            if (utilityScope == null)
            {
                return null;
            }
            return new UtilityScopeDto { Id = utilityScope.Id, Title = utilityScope.Title, Description = utilityScope.Description };
        }
        public async Task<List<UtilityScopeDto>> GetUtilityScopesByCriteriaAsync(UtilityScopeSearchCriteria criteria)
        {
            var query = _context.UtilityScopes
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
                query = query.Where(us => _context.RoleUtilityScopes
                    .Any(rus => rus.UtilityScopeId == us.Id && rus.RoleId == criteria.RoleId));
            }

            if (!string.IsNullOrEmpty(criteria.UserId))
            {
                var user = await _userManager.FindByIdAsync(criteria.UserId);
                if (user != null)
                {
                    var userRoles = await _userManager.GetRolesAsync(user);
                    var userRoleIds = await _context.Roles
                        .Where(r => userRoles.Contains(r.Name))
                        .Select(r => r.Id)
                        .ToListAsync();

                    if (userRoleIds.Any())
                    {
                        query = query.Where(us => _context.RoleUtilityScopes
                            .Any(rus => rus.UtilityScopeId == us.Id && userRoleIds.Contains(rus.RoleId)));
                    }
                }
            }

            if (criteria.PermissionId.HasValue)
            {
                query = query.Where(us => _context.UtilityScopePermissions
                    .Any(usp => usp.UtilityScopeId == us.Id && usp.PermissionId == criteria.PermissionId.Value));
            }

            var items = await query.ToListAsync();

            return items;
        }


        public async Task<bool> CreateUtilityScopeAsync(CreateUtilityScopeDto utilityScopeDto, CancellationToken cancellationToken)
        {
            try
            {
                var utilityScope = new UtilityScope
                {
                    Title = utilityScopeDto.Title,
                    Description = utilityScopeDto.Description
                };

                _context.UtilityScopes.Add(utilityScope);
                await _context.SaveChangesAsync(cancellationToken);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating utility scope:", ex);
            }
        }

        public async Task<bool> AssignUtilityScopeToRoleAsync(string roleId, Guid utilityScopeId, CancellationToken cancellationToken)
        {

            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                throw new ApplicationException("Role unvalid!");
            }
            if (!await ValidateUtilityScopeId(utilityScopeId))
            {
                throw new ApplicationException("Utility Scope unvalid!");
            }
            if (!await _context.RoleUtilityScopes.AnyAsync(rus => rus.RoleId == roleId && rus.UtilityScopeId == utilityScopeId))
            {
                _context.RoleUtilityScopes.Add(new RoleUtilityScope
                {
                    RoleId = roleId,
                    UtilityScopeId = utilityScopeId
                });

                await _context.SaveChangesAsync(cancellationToken);
            }

            return true;
        }
        private async Task<bool> ValidateUtilityScopeId(Guid id)
        {
            return await _context.UtilityScopes.AnyAsync(us => us.Id == id);
        }
        public async Task<bool> AssignPermissionsToUtilityScopeAsync(Guid utilityScopeId, List<Guid> permissionIds, CancellationToken cancellationToken)
        {

            foreach (var permissionId in permissionIds)
            {

                if (!await _context.UtilityScopePermissions.AnyAsync(usp => usp.UtilityScopeId == utilityScopeId && usp.PermissionId == permissionId))
                {
                    _context.UtilityScopePermissions.Add(new UtilityScopePermission
                    {
                        UtilityScopeId = utilityScopeId,
                        PermissionId = permissionId
                    });
                }
            }

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
        public async Task<bool> UnassignPermissionsFromUtilityScopeAsync(Guid utilityScopeId, List<Guid> permissionIds, CancellationToken cancellationToken)
        {

            foreach (var permissionId in permissionIds)
            {

                var utilityScopePermissions = await _context.UtilityScopePermissions
                    .Where(usp => usp.UtilityScopeId == utilityScopeId && usp.PermissionId == permissionId)
                    .ToListAsync();

                _context.UtilityScopePermissions.RemoveRange(utilityScopePermissions);
            }

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
        public async Task<bool> UnassignUtilityScopeFromRoleAsync(string roleId, Guid utilityScopeId, CancellationToken cancellationToken)
        {

            var roleUtilityScope = await _context.RoleUtilityScopes
                .FirstOrDefaultAsync(rus => rus.RoleId == roleId && rus.UtilityScopeId == utilityScopeId);

            if (roleUtilityScope != null)
            {
                _context.RoleUtilityScopes.Remove(roleUtilityScope);
                await _context.SaveChangesAsync(cancellationToken);
            }

            return true;
        }
        public async Task<bool> UpdateUtilityScopeAsync(Guid id, string? title, string? description, CancellationToken cancellationToken = default)
        {
            var utilityScope = await _context.UtilityScopes.FindAsync(id);
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

            _context.UtilityScopes.Update(utilityScope);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
        #endregion


    }
}
