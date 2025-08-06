namespace SchoolSaas.Application.Common.Models
{
    public class GenderStatisticResponseDto
    {
        public List<GenderStatistics> GenderStatistics { get; set; }
    }

    public class GenderStatistics
    {
        public string Gender { get; set; }
        public int Total { get; set; }
        public double Percentage { get; set; }
    }
}
