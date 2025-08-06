namespace SchoolSaas.Domain.Common.DataObjects.Student
{
    public class BulkStudentUpdateDTO
    {
        public List<Guid> StudentIds { get; set; } = new();
        public StudentUpdateDTO UpdateData { get; set; } = new();
    }
}
