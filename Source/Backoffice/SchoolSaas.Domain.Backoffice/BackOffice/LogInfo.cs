using SchoolSaas.Domain.Common.Entities;
using SchoolSaas.Domain.Common.Enums;

namespace SchoolSaas.Domain.Backoffice.BackOffice
{
    public class LogInfo : BaseEntity<Guid>
    {
        public Guid CallId { get; set; }
        public string Thread { get; set; }
        public LogLevelEnum Level { get; set; }
        public LogEventTypeEnum EventType { get; set; }
        public string Method { get; set; }
        public string Assembly { get; set; }
        public int? ErrorCode { get; set; }
        public double? CallDuration { get; set; }
        public int? ReturnedLines { get; set; }
        public string? Message { get; set; }
        public string? Exception { get; set; }
        public string? Provider { get; set; }
        public Guid? RootCallId { get; set; }
    }
}
