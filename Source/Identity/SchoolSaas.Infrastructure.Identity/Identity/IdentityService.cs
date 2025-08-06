using SchoolSaas.Application.Common.Exceptions;
using SchoolSaas.Application.Common.Extensions;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Application.Common.Models;
using SchoolSaas.Application.Identity.DataTransferObjects;
using SchoolSaas.Domain.Common.Constants;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using SchoolSaas.Infrastructure.Identity.Context;
using SchoolSaas.Infrastructure.Identity.Identity.Entities;
using System.Text;
using static SchoolSaas.Domain.Common.Constants.AuthorizationConstants;
using ApplicationException = SchoolSaas.Application.Common.Exceptions.ApplicationException;
using SchoolSaas.Domain.Common.DataObjects.Common;

namespace SchoolSaas.Infrastructure.Identity.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly IdentityContext _dbContext;
        private readonly IEmailSender _emailSender;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly ICurrentUserService _currentUserService;
        private readonly ITenantAccessor _tenantAccessor;


        public IdentityService(IdentityContext dbContext,
            IEmailSender emailSender, ICurrentUserService currentUserService, ITenantAccessor tenantAccessor, ManagerPicker managerPicker)
        {
            _userManager = managerPicker.GetUserManager(dbContext);
            _roleManager = managerPicker.GetRoleManager(dbContext);
            _dbContext = dbContext;
            _emailSender = emailSender;
            _currentUserService = currentUserService;
            _tenantAccessor = tenantAccessor;
        }

        public async Task<string> GetUserNameAsync(string userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);

            return user?.UserName;
        }

        public async Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password)
        {
            var user = new ApplicationUser
            {
                UserName = userName,
                Email = userName,
                EmailConfirmed = false,
            };

            var result = await _userManager.CreateAsync(user, password);

            return (result.ToApplicationResult(), user.Id);
        }

        public async Task<Result> DeleteUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            NotFoundException.ThrowIfNull(nameof(Application.Identity.DataTransferObjects.UserDto), userId);

            var roleNames = user.UserRoles.Any() ? user.UserRoles.Select(e => e.Role.Name).ToList() : new List<string>();

            //if (IsGeneralManager() && roleNames.First() == Roles.SuperAdmins || IsManager())
            //    throw new ApplicationException($"vous n'avez pas la permission de supprimer l'utilisateur {user.FirstName} {user.LastName}");

            return await DeleteUserAsync(user);
        }

        public async Task<Domain.Common.DataObjects.Common.UserDto> GetUserByIdAsync(string userId)
        {
            var user = await _dbContext.Users.Where(e => e.Id == userId)
                .Include(e => e.UserRoles)
                .ThenInclude(ur => ur.Role)
                .Select(e => MapAppUser(e))
                .FirstOrDefaultAsync();

            return user;
        }

        public Task<PagedResult<Domain.Common.DataObjects.Common.UserDto>> GetUsersAsync(int? page, int? size, UserCriteria criteria)
        {
            if (IsSuperAdmin())
            {
                criteria.RoleName = Roles.SuperAdmins;
            }

            var userQuery = _dbContext.Set<ApplicationUser>().IgnoreQueryFilters().AsNoTracking();
            userQuery = _dbContext.ApplySpecification(userQuery, new UserSearchSpecification(criteria));

            //if (!IsSuperAdmin())
            //{
            //    userQuery = userQuery.Where(e => (e.TenantId == _tenantAccessor.GetTenantId() || e.BackofficeId == _tenantAccessor.GetTenantId()) && e.Id != _currentUserService.UserId);
            //}

            var users = userQuery
                .OrderByDescending(t => t.Created)
                .Select(e => MapAppUser(e))
                .GetPaged(page.GetValueOrDefault(1),
                    size.GetValueOrDefault(CoreConstants.DefaultPageSize));

            return users;
        }
        public Task<List<Domain.Common.DataObjects.Common.UserDto>> GetUsersByCriteriaAsync(UserCriteria criteria)
        {
            var userQuery = _dbContext.ApplySpecification(new UserSearchSpecification(criteria));
            var users = userQuery
                .Select(e => MapAppUser(e))
                .OrderByDescending(t => t.Created).ToListAsync();
            return users;
        }

        public Task<List<Domain.Common.DataObjects.Common.UserDto>> GetUsersAsync()
        {
            var userQuery = _dbContext.Users.AsNoTracking();
            var users = userQuery
                .Select(appUser => new Domain.Common.DataObjects.Common.UserDto()
                {
                    Id = appUser.Id,
                    UserName = appUser.UserName,
                    FirstName = appUser.FirstName,
                    LastName = appUser.LastName,
                    RoleNames = appUser.UserRoles.Any() ? appUser.UserRoles.Select(e => e.Role.Name).ToList() : new List<string>(),
                    Email = appUser.Email,
                    PhoneNumber = appUser.PhoneNumber,
                    Created = appUser.Created,
                    CreatedBy = appUser.CreatedBy,
                    LastModified = appUser.LastModified,
                    LastModifiedBy = appUser.LastModifiedBy,
                })
                .OrderByDescending(t => t.Created).ToListAsync();
            return users;
        }

        public async Task<Domain.Common.DataObjects.Common.UserDto> CreateUserAsync(Domain.Common.DataObjects.Common.UserDto user, string password = null)
        {
            var appUser = MapUser(user);

            if (IsSuperAdmin())
            {
                if (!user.RoleNames.Contains(Roles.SuperAdmins) && !user.RoleNames.Contains(Roles.SuperAdmins))
                {
                    user.RoleNames = new List<string>() { Roles.SuperAdmins };
                }
            }
            else
            {
                user.RoleNames.Remove(Roles.SuperAdmins);
            }

            if (string.IsNullOrEmpty(user.UserName))
            {
                if (!user.RoleNames.Contains(Roles.SuperAdmins)
                    && !string.IsNullOrEmpty(user.FirstName) && !string.IsNullOrEmpty(user.LastName))
                {
                    appUser.UserName = user.FirstName + " " + user.LastName;
                }
                else
                {
                    appUser.UserName = user.Email;
                }
            }
            else
            {
                appUser.UserName = user.UserName;
            }

            appUser.Id = Guid.NewGuid().ToString();
            appUser.UserName = appUser.UserName;
            appUser.Created = DateTime.Now;
            appUser.EmailConfirmed = true;

            if (string.IsNullOrEmpty(password))
            {
                password = PasswordGenerator.Generate();
            }

            var usernameExists = _userManager.Users.Any(u => u.UserName.ToLower().Equals(appUser.UserName.ToLower()));
            if (usernameExists)
            {
                throw new ApplicationException($"Le nom d'utilisateur saisi existe déjà: {appUser.UserName}");
            }

            var emailAlreadyExists = _userManager.Users.Any(u => u.Email.ToLower().Equals(appUser.Email.ToLower()));
            if (emailAlreadyExists)
            {
                throw new ApplicationException($"L'adresse email saisie existe déjà: {appUser.Email}");
            }


            var result = await _userManager.CreateAsync(appUser, password);

            if (result.Succeeded)
            {
                foreach (var roleName in user.RoleNames)
                {
                    await _userManager.AddToRoleAsync(appUser, roleName);
                }

                //if (!string.IsNullOrEmpty(appUser.BackofficeId))
                //{
                //    await _userManager.AddClaimAsync(appUser, new Claim(AuthorizationConstants.ClaimTypes.Backoffice, appUser.BackofficeId));
                //}

                try
                {
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(appUser);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    Domain.Common.DataObjects.Common.UserCreateDto Mailuser = new Domain.Common.DataObjects.Common.UserCreateDto
                    {
                        Id = appUser.Id,
                        UserName = appUser.UserName,
                        Email = appUser.Email,
                        FirstName = appUser.FirstName,
                        LastName = appUser.LastName,
                        Password = password,
                        Token = code
                    };

                    await _emailSender.SendEmailAsync<Domain.Common.DataObjects.Common.UserCreateDto>(Mailuser.Email, "Compte EConsulatMA", TemplatesNames.Emails.Register, Mailuser);

                }
                catch (Exception)
                {
                }
                ;
            }
            else
            {
                throw new ApplicationException("Echec de création de l'utilisateur : " + string.Join(",", result.ToApplicationResult().Errors));
            }

            user.Id = appUser.Id;
            user.UserName = appUser.UserName;
            user.Created = appUser.Created;
            user.CreatedBy = appUser.CreatedBy;
            user.LastModified = appUser.LastModified;
            user.LastModifiedBy = appUser.LastModifiedBy;
            user.HashedPassword = _userManager.PasswordHasher.HashPassword(appUser, password);
            return user;
        }

        public async Task<Domain.Common.DataObjects.Common.UserDto> UpdateUserAsync(string userId, Domain.Common.DataObjects.Common.UserDto user)
        {
            var entity = await _userManager.FindByIdAsync(userId);

            NotFoundException.ThrowIfNull(nameof(Domain.Common.DataObjects.Common.UserDto), userId);

            if (user.RoleNames?.Any() == true)
            {
                user.RoleNames.Remove(Roles.SuperAdmins);
                if (user.RoleNames?.Any() == true)
                {
                    var roles = await _userManager.GetRolesAsync(entity);

                    await _userManager.RemoveFromRolesAsync(entity, roles);
                    await _userManager.AddToRolesAsync(entity, user.RoleNames);
                }
            }

            var updateUser = MapNewData(entity, user);
            updateUser.LastModified = DateTime.Now;

            var result = await _userManager.UpdateAsync(updateUser);

            if (result.Succeeded)
                return MapAppUser(entity);

            return null;
        }

        public async Task<Result> ChangePasswordAsync(string userId, string oldPassword, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId);

            NotFoundException.ThrowIfNull(nameof(Domain.Common.DataObjects.Common.UserDto), userId);

            var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);

            return result.ToApplicationResult();
        }

        public async Task<string> ResetPasswordAsync(string userId)
        {
            ApplicationUser appUser = await _userManager.FindByIdAsync(userId);

            NotFoundException.ThrowIfNull(appUser);

            //StringWriter myWriter = new StringWriter();
            //HttpUtility.HtmlDecode(resetTokenCode, myWriter);
            //string myDecodedString = myWriter.ToString();
            var newPassword = PasswordGenerator.Generate();
            var token = await _userManager.GeneratePasswordResetTokenAsync(appUser);

            var result = await _userManager.ResetPasswordAsync(appUser, token, newPassword);

            if (result.Succeeded)
            {
                UserResetDto Mailuser = new UserResetDto
                {
                    FirstName = appUser.FirstName,
                    LastName = appUser.LastName,
                    Password = newPassword,
                };
                try
                {
                    await _emailSender.SendEmailAsync<UserResetDto>(appUser.Email, "Password Reset", TemplatesNames.Emails.ResetPassword, Mailuser);
                }
                catch (Exception)
                {

                }
                return newPassword;
            }


            throw new ApplicationException("");
        }

        public async Task<bool> VerifyUserToken(string token, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var purpose = UserManager<ApplicationUser>.ResetPasswordTokenPurpose;

            return await _userManager.VerifyUserTokenAsync(user, TokenOptions.DefaultProvider, purpose, token);
        }

        public async Task<string> GetUserToken(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var purpose = UserManager<ApplicationUser>.ResetPasswordTokenPurpose;

            return await _userManager.GenerateUserTokenAsync(user, TokenOptions.DefaultProvider, purpose);
        }
        public async Task<bool> DeleteRoleAsync(string roleId)
        {
            var existingRole = await _roleManager.FindByIdAsync(roleId);

            if (existingRole == null)
            {
                return false;
            }

            var result = await _roleManager.DeleteAsync(existingRole);

            if (result.Succeeded)
            {
                return true;
            }
            else
            {
                var errors = result.Errors.Select(e => e.Description);
                //loggin should be added
                return false;
            }
        }

        private async Task<Result> DeleteUserAsync(ApplicationUser user)
        {
            if (_currentUserService.UserId == user.Id)
                throw new ApplicationException("Invalid operation.");

            var currentRoles = _currentUserService.RoleNames ?? new List<string>();
            if (user.UserRoles.Any(ur => ur.Role.Name == Roles.SuperAdmins))
                throw new ApplicationException("L'administrateur d'un backoffice ne peut pas être supprimé.");

            var result = await _userManager.DeleteAsync(user);

            return result.ToApplicationResult();
        }

        public ApplicationUser MapNewData(ApplicationUser entity, Domain.Common.DataObjects.Common.UserDto user)
        {
            if (!string.IsNullOrWhiteSpace(user.FirstName)) entity.FirstName = user.FirstName;
            if (!string.IsNullOrWhiteSpace(user.LastName)) entity.LastName = user.LastName;
            if (!string.IsNullOrWhiteSpace(user.UserName)) entity.UserName = user.UserName;
            if (!string.IsNullOrWhiteSpace(user.PhoneNumber)) entity.PhoneNumber = user.PhoneNumber;

            return entity;
        }

        public async Task<Domain.Common.DataObjects.Common.UserDto> GetUserByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            NotFoundException.ThrowIfNull(nameof(Domain.Common.DataObjects.Common.UserDto), email);

            return MapAppUser(user);
        }
        public async Task<bool> AnyUserByPhoneAsync(string phone)
        {
            return await _userManager.Users.AnyAsync(u => u.PhoneNumber == phone);
        }
        public async Task<List<Domain.Common.DataObjects.Common.UserDto>> GetUserByRolesAsync(List<string> roles)
        {
            var users = new List<Domain.Common.DataObjects.Common.UserDto>();

            foreach (var role in roles)
            {
                var roleuser = (await _userManager.GetUsersInRoleAsync(role)).Select(e => MapAppUser(e)).ToList();
                roleuser.ForEach(r => r.RoleNames.Add(role));
                users.AddRange(roleuser);
            }
            return users;
        }

        public async Task<bool> CreateRoleAsync(string roleName)
        {
            var existingRole = _roleManager.FindByNameAsync(roleName);
            if (existingRole.Result != null)
            {
                return false;
            }
            var role = new ApplicationRole
            {
                Id = Guid.NewGuid().ToString(),
                Name = roleName,
                NormalizedName = roleName.Normalize()

            };

            var result = await _roleManager.CreateAsync(role);
            return result.Succeeded;
        }
        public async Task<List<RoleDto>> GetRolesAsync()
        {
            return await _roleManager.Roles
                .Where(r => r.Name != Roles.SuperAdmins)
                .Select(r => new RoleDto
                {
                    Id = r.Id,
                    Name = r.Name
                }).ToListAsync();
        }

        public async Task<Result> UpdateUserRoleAsync(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            NotFoundException.ThrowIfNull(nameof(Domain.Common.DataObjects.Common.UserDto), userId);

            var roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                throw new NotFoundException($"Role {roleName} does not exist.");
            }

            var userRole = _dbContext.UserRoles.FirstOrDefault(c => c.UserId == user.Id);
            if (userRole != null)
            {
                var roles = _dbContext.Roles.Where(c => c.Id == userRole.RoleId);
                foreach (var role in roles)
                {
                    await _userManager.RemoveFromRoleAsync(user, role.Name);
                }
            }

            var result = await _userManager.AddToRoleAsync(user, roleName);
            return result.ToApplicationResult();
        }
        public async Task<bool> UpdateRoleAsync(string roleId, string newRoleName)
        {
            var existingRole = await _roleManager.FindByIdAsync(roleId);

            if (!string.IsNullOrEmpty(newRoleName) && existingRole.Name != newRoleName)
            {
                existingRole.Name = newRoleName;
                existingRole.NormalizedName = _roleManager.NormalizeKey(newRoleName);
            }

            var result = await _roleManager.UpdateAsync(existingRole);

            if (result.Succeeded)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public async Task<List<string>> GetUserClaimesAsync(string userId, string claimType)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return new List<string>();

            var claimes = (await _userManager.GetClaimsAsync(user)).Select(e => e.Value).ToList();

            var roleNames = await _userManager.GetRolesAsync(user);

            foreach (var roleName in roleNames)
                claimes.AddRange((await _roleManager.GetClaimsAsync(await _roleManager.FindByNameAsync(roleName))).Select(e => e.Value).ToList());

            return claimes;
        }

        public async Task<Unit> ToggleUserStatusAsync(string userId, bool isActive)
        {
            var entity = await _userManager.FindByIdAsync(userId);

            NotFoundException.ThrowIfNull(nameof(Domain.Common.DataObjects.Common.UserDto), userId);

            //entity.IsDeactivated = !isActive; TODO : ADD activation logic

            var result = await _userManager.UpdateAsync(entity);

            return Unit.Value;
        }

        public async Task<List<string>> GetRoleClaimsAsync(string roleId, string claimType)
        {
            var role = await _roleManager.FindByIdAsync(roleId);

            if (role == null)
                return new List<string>();

            if (string.IsNullOrEmpty(claimType))
                return (await _roleManager.GetClaimsAsync(role)).Select(e => e.Value).ToList();
            else
                return (await _roleManager.GetClaimsAsync(role)).Where(e => e.Type == claimType).Select(e => e.Value).ToList();
        }
        public async Task<List<RoleDto>> GetRolesByCriteriaAsync(GetRoleByCriteriaDto criteria)
        {
            var roleQuery = _dbContext.Roles.AsQueryable();
            if (!string.IsNullOrEmpty(criteria.RoleId))
            {
                roleQuery = roleQuery.Where(r => r.Id.Contains(criteria.RoleId));
            }
            if (!string.IsNullOrEmpty(criteria.RoleName))
            {
                roleQuery = roleQuery.Where(r => r.Name.Contains(criteria.RoleName));
            }

            if (!string.IsNullOrEmpty(criteria.UserId))
            {
                var userRoles = await _dbContext.UserRoles
                    .Where(ur => ur.UserId == criteria.UserId)
                    .Select(ur => ur.RoleId)
                    .ToListAsync();
                roleQuery = roleQuery.Where(r => userRoles.Contains(r.Id));
            }

            if (!string.IsNullOrEmpty(criteria.UserName) ||
                !string.IsNullOrEmpty(criteria.UserFn) ||
                !string.IsNullOrEmpty(criteria.UserLn))
            {
                var usersQuery = _dbContext.Users.AsQueryable();

                if (!string.IsNullOrEmpty(criteria.UserName))
                {
                    usersQuery = usersQuery.Where(u => u.UserName.Contains(criteria.UserName));
                }

                if (!string.IsNullOrEmpty(criteria.UserFn))
                {
                    usersQuery = usersQuery.Where(u => u.FirstName.Contains(criteria.UserFn));
                }

                if (!string.IsNullOrEmpty(criteria.UserLn))
                {
                    usersQuery = usersQuery.Where(u => u.LastName.Contains(criteria.UserLn));
                }

                var userRoles = await usersQuery
                    .Join(_dbContext.UserRoles, u => u.Id, ur => ur.UserId, (u, ur) => ur.RoleId)
                    .Distinct()
                    .ToListAsync();

                roleQuery = roleQuery.Where(r => userRoles.Contains(r.Id));
            }

            var roles = await roleQuery
                .Select(r => new RoleDto
                {
                    Id = r.Id,
                    Name = r.Name
                })
                .ToListAsync();

            return roles;
        }

        private static Domain.Common.DataObjects.Common.UserDto MapAppUser(ApplicationUser appUser)
        {
            return new Domain.Common.DataObjects.Common.UserDto()
            {
                Id = appUser.Id,
                UserName = appUser.UserName,
                FirstName = appUser.FirstName,
                LastName = appUser.LastName,
                RoleNames = appUser.UserRoles.Any() ? appUser.UserRoles.Select<ApplicationUserRole, string>(e => e.Role.Name).ToList() : new List<string>(),
                Email = appUser.Email,
                PhoneNumber = appUser.PhoneNumber,
                Created = appUser.Created,
                CreatedBy = appUser.CreatedBy,
                LastModified = appUser.LastModified,
                LastModifiedBy = appUser.LastModifiedBy,
            };
        }

        private static ApplicationUser MapUser(Domain.Common.DataObjects.Common.UserDto user)
        {
            return new ApplicationUser()
            {
                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };
        }

        private bool IsSuperAdmin()
        {
            var currentRoles = _currentUserService.RoleNames ?? new List<string>();
            return currentRoles.Any(e => e == Roles.SuperAdmins);
        }
    }
}