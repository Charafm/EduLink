namespace SchoolSaas.Application.Common.Models
{
    public class AgeStatisticResponseDto
    {
        public List<AgeStatistics> AgeStatistics { get; set; }
    }
    public class AgeStatistics
    {
        public string age { get; set; }
        public int value { get; set; }
    }
}
