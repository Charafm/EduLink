using SchoolSaas.Domain.Common.DataObjects.Parent;
using SchoolSaas.Domain.Common.DataObjects.Staff;
using SchoolSaas.Domain.Common.DataObjects.Student;
using SchoolSaas.Domain.Common.DataObjects.Teacher;

namespace SchoolSaas.Domain.Common.DataObjects.Common
{
    public class ProfileDto { 
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string[] Roles { get; set; }
        public ParentDTO? Parent { get; set; }
        public StudentDTO? Student { get; set; }
        public StaffDTO? Staff  { get; set; }
        public TeacherDTO? Teacher { get; set; }
        /*…*/ }
}
