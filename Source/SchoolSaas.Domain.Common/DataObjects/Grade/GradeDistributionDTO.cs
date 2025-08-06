using SchoolSaas.Domain.Common.Enums;

namespace SchoolSaas.Domain.Common.DataObjects.Grade
{
    public class GradeDistributionDTO
    {
        public GradeTypeEnum GradeType { get; set; }
        public int Count { get; set; }
        public double Average { get; set; }
    }
}
