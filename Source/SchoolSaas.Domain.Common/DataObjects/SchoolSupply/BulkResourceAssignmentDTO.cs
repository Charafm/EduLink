using SchoolSaas.Domain.Common.DataObjects.Grade;

namespace SchoolSaas.Domain.Common.DataObjects.SchoolSupply
{
    public class BulkResourceAssignmentDTO
    {
        public List<GradeResourceDTO> Assignments { get; set; } = new();
    }
}
