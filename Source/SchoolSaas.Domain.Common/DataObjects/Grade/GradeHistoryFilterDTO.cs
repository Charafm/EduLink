namespace SchoolSaas.Domain.Common.DataObjects.Grade
{
    public class GradeHistoryFilterDTO
    {
        public Guid GradeId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }

}
