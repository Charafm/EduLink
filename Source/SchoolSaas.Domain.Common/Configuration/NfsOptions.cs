namespace SchoolSaas.Domain.Common.Configuration
{
    public class NfsOptions
    {
        public bool IsEnabled { get; set; }
        public string MountDir { get; set; }
        public string StaticDocsPath { get; set; }
        public string MountDirBackup { get; set; }
        public string StaticDocsPathBackup { get; set; }
        public string HealthCheckFile { get; set; }
        public int HealthCheckInterval { get; set; }

    }
}