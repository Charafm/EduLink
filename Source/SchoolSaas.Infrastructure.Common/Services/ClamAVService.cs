using SchoolSaas.Application.Common.Interfaces;
using nClam;

namespace SchoolSaas.Infrastructure.Common.Services
{
    public class ClamAVService : IFileScanService
    {
        public async Task ScanFile(byte[] data)
        {
            var clam = new ClamClient("localhost", 3310);
            var pingResult = await clam.TryPingAsync();

            if (!pingResult)
            {
                throw new Exception("ClamAV server not responding.");
            }

            var scanResult = await clam.SendAndScanFileAsync(data);

            switch (scanResult.Result)
            {
                case ClamScanResults.VirusDetected:
                    throw new Exception("Virus found! : " + scanResult.InfectedFiles.First().VirusName);
                case ClamScanResults.Error:
                    throw new Exception("Error occured! Error: " + scanResult.RawResult);
            }
        }
    }
}