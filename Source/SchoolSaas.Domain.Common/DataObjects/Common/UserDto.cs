using SchoolSaas.Domain.Common.Entities;

namespace SchoolSaas.Domain.Common.DataObjects.Common
{
    public class UserDto : IDeletableEntity
    {
        public string Id { get; set; }
        public string? Email { get; set; }
        public string UserName { get; set; } = null!;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public bool? EmailConfirmed { get; set; }
        public bool? PhoneNumberConfirmed { get; set; }

        public List<string> RoleNames { get; set; }
        public string? Function { get; set; }
        public List<string>? Permissions { get; set; } = null!;
        public string? CreatedBy { get; set; }
        public DateTime? Created { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public bool? IsDeleted { get; set; }

        public string HashedPassword { get; set; }
    }
}