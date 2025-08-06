namespace SchoolSaas.Domain.Common.DataObjects.SchoolSupply
{
    public class SchoolSupplyFilterDTO
    {
        public string SearchTerm { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}
