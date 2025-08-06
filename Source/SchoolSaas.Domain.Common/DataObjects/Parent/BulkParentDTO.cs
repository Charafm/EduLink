namespace SchoolSaas.Domain.Common.DataObjects.Parent
{
    public class BulkParentDTO
    {
        public List<ParentDTO> Records { get; set; } = new();
        public string InitiatedBy { get; set; }
    }
}
