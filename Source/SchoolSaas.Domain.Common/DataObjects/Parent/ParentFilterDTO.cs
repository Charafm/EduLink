namespace SchoolSaas.Domain.Common.DataObjects.Parent
{
    public class ParentFilterDTO
    {
        //public Guid StudentId { get; set; }
        public string SearchTerm { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}
