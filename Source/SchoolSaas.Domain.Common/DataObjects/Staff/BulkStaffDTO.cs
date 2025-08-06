namespace SchoolSaas.Domain.Common.DataObjects.Staff
{
    public class BulkStaffDTO
    {
        public List<StaffDTO> Records { get; set; } = new();
        public string InitiatedBy { get; set; }
    }
}
