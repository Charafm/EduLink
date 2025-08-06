namespace SchoolSaas.Domain.Common.DataObjects.SchoolSupply
{
    public class BulkSchoolSupplyDTO
    {
        public List<SchoolSupplyDTO> Supplies { get; set; } = new();
    }
}
