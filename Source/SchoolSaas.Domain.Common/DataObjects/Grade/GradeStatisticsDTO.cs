namespace SchoolSaas.Domain.Common.DataObjects.Grade
{
    public class GradeStatisticsDTO
    {
        public double AverageScore { get; set; }
        public double MaxScore { get; set; }
        public double MinScore { get; set; }
        public List<GradeDistributionDTO> GradeDistribution { get; set; } = new();
    }
}
