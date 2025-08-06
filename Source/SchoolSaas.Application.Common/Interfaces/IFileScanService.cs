namespace SchoolSaas.Application.Common.Interfaces
{
    public interface IFileScanService
    {
        Task ScanFile(byte[] data);
    }
}