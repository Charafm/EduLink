using SchoolSaas.Application.Common.Mappings;

namespace SchoolSaas.Application.Identity.DataTransferObjects
{
    public class UserUpdateDto : IMapFrom<Domain.Common.DataObjects.Common.UserDto>, IMapTo<Domain.Common.DataObjects.Common.UserDto>
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public List<string> RoleNames { get; set; }

    }
}