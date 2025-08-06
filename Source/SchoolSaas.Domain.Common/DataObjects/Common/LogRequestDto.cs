namespace SchoolSaas.Domain.Common.DataObjects.Common
{
    public class LogRequestDto
    {
        public Guid? RootCallId { get; set; }
        public string CallId { get; set; }
        public string Thread { get; set; }
        public string Level { get; set; }
        public int EventType { get; set; }
        public string MethodName { get; set; }
        public string AssemblyName { get; set; }
        public string Message { get; set; }
        public string Exception { get; set; }

        
    }
}
