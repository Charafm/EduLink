using SchoolSaas.Application.Common.Specifications;
using SchoolSaas.Domain.Common.Constants;
using SchoolSaas.Infrastructure.Identity.Identity.Entities;
using SchoolSaas.Domain.Common.DataObjects.Common;

namespace SchoolSaas.Infrastructure.Identity
{
    public class UserSearchSpecification : BaseSpecification<ApplicationUser>
    {
        public UserSearchSpecification(UserCriteria criteria)
        {
            AddInclude(u => u.UserRoles);
            AddInclude("UserRoles.Role");

            AddCriteria(u => !(u.IsDeleted ?? false));

            if (!string.IsNullOrEmpty(criteria.RoleName))
            {
                if (criteria.RoleName == AuthorizationConstants.Roles.SuperAdmins)
                    AddCriteria(s => s.UserRoles.Any(ur => ur.Role.Name == AuthorizationConstants.Roles.SuperAdmins));

            }

            if (!string.IsNullOrEmpty(criteria.UserName))
                AddCriteria(s => s.UserName.StartsWith(criteria.UserName));

            if (!string.IsNullOrEmpty(criteria.Email))
                AddCriteria(s => s.Email.StartsWith(criteria.Email));

            if (!string.IsNullOrEmpty(criteria.LastName))
                AddCriteria(s => !string.IsNullOrEmpty(s.LastName) && s.LastName.StartsWith(criteria.LastName));

            if (!string.IsNullOrEmpty(criteria.FirstName))
                AddCriteria(s => !string.IsNullOrEmpty(s.FirstName) && s.FirstName.StartsWith(criteria.FirstName));
        }
    }
}