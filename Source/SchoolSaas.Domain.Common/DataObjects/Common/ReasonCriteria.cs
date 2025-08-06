namespace SchoolSaas.Domain.Common.DataObjects.Common
{
    public class ReasonCriteria
    {
        public Guid? ModuleId { get; set; }
        public Guid? RequestTypeId { get; set; }
        public string? ReasonTitle { get; set; }
        public string? ReasonTitleAr { get; set; }
        public string? RequestTypeAbrreviation { get; set; }
        public bool? Status { get; set; }
        public int? PageSize { get; set; }
        public int? Page { get; set; }
    }
}
