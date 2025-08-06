namespace SchoolSaas.Domain.Common.DataObjects.Student
{
    public class StudentHistoryFilterDTO
    {
        public Guid StudentId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}
