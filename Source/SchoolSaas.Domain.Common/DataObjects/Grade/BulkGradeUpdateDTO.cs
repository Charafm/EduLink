namespace SchoolSaas.Domain.Common.DataObjects.Grade
{
    public class BulkGradeUpdateDTO
    {
        public List<Guid> GradeIds { get; set; } = new();
        public GradeUpdateDTO UpdateData { get; set; }
    }
}
