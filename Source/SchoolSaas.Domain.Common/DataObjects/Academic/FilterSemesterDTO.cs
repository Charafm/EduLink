namespace SchoolSaas.Domain.Common.DataObjects.Academic
{
    public class FilterSemesterDTO
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string SortBy { get; set; } = "Name"; 
        public bool IsAscending { get; set; } = true;
    }
}
