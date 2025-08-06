using Microsoft.Extensions.Options;
using SchoolSaas.Application.Common.Interfaces;
using SchoolSaas.Domain.Common.Configuration;
using SchoolSaas.Domain.Common.Extensions;

namespace SchoolSaas.Infrastructure.Common.Services
{
    public class LocalStorage : IStorage
    {
        private readonly NfsOptions _nfsOptions;
        private bool _useBackup;
        public LocalStorage(IOptions<NfsOptions> nfsOptions)
        {
            _nfsOptions = nfsOptions.Value;
            _useBackup = false;
            StartHealthCheck();
        }
        public async Task<string> SaveFileAsync(byte[] data, string absolutePath, string fileName)
        {
            if (!Directory.Exists(absolutePath.Trim()))
                Directory.CreateDirectory(absolutePath.Trim());

            await File.WriteAllBytesAsync(absolutePath.Trim().AddSuffix(fileName.Trim()), data);

            return absolutePath.Trim().AddSuffix(fileName.Trim());
        }

        public Task<string> SaveFileAsync(Stream stream, string absolutePath, string fileName)
        {
            try
            {
                string serverPath = GetActiveServerPath();


                absolutePath = Path.Combine(serverPath, absolutePath.Trim());
                if (!Directory.Exists(absolutePath.Trim()))
                    Directory.CreateDirectory(absolutePath.Trim());

                var filePath = Path.Combine(absolutePath, fileName.Trim());

                if (File.Exists(filePath))
                    File.Delete(filePath);

                using (var fileStream = File.Create(filePath))
                {
                    stream.Position = 0;

                    stream.CopyTo(fileStream);

                    return Task.FromResult(filePath);
                }
            }
            catch (Exception)
            {
                throw;
            }

        }



        public Task<bool> DeleteFileAsync(string fileAbsolutePath)
        {
            if (File.Exists(fileAbsolutePath.Trim()))
                File.Delete(fileAbsolutePath.Trim());

            return Task.FromResult(true);
        }

        public async Task<byte[]?> GetFileAsBytesAsync(string fileAbsolutePath)
        {
            if (File.Exists(fileAbsolutePath.Trim()))
            {
                return await File.ReadAllBytesAsync(fileAbsolutePath.Trim());
            }

            return null;
        }

        public Task<Stream?> GetFileAsync(string fileAbsolutePath)
        {
            if (File.Exists(fileAbsolutePath.Trim()))
            {
                var stream = new StreamReader(fileAbsolutePath.Trim()).BaseStream;

                stream.Position = 0;

                return Task.FromResult(stream);
            }

            return null;
        }

        public Task<List<string>> GetFilesAsync(string absolutePath, string folderRelativePath)
        {
            if (Directory.Exists(absolutePath.Trim().AddSuffix(folderRelativePath.Trim())))
                return Task.FromResult(Directory.GetFiles(absolutePath.Trim().AddSuffix(folderRelativePath.Trim()))
                    .ToList());

            return null;
        }

        public Task<string> CreateFolderAsync(string absolutePath, string folderRelativePath)
        {
            return Task.FromResult(absolutePath.Trim().AddSuffix(folderRelativePath.Trim()));
        }

        public Task<bool> DeleteFolderAsync(string absolutePath, string folderRelativePath)
        {
            if (Directory.Exists(absolutePath.Trim().AddSuffix(folderRelativePath.Trim())))
                Directory.Delete(absolutePath.Trim().AddSuffix(folderRelativePath.Trim()), true);

            return Task.FromResult(true);
        }

        public Task<List<string>> GetFoldersAsync(string absolutePath, string folderRelativePath)
        {
            if (Directory.Exists(absolutePath.Trim().AddSuffix(folderRelativePath.Trim())))
                return Task.FromResult(Directory
                    .GetDirectories(absolutePath.Trim().AddSuffix(folderRelativePath.Trim())).ToList());

            return null;
        }

        private void StartHealthCheck()
        {
            Task.Run(async () =>
            {
                while (true)
                {
                    await CheckHealth();
                    await Task.Delay(_nfsOptions.HealthCheckInterval);
                }
            });
        }

        private async Task CheckHealth()
        {
            try
            {
                string healthCheckPath = Path.Combine(_nfsOptions.MountDir);
                if (Directory.Exists(healthCheckPath))
                {
                    // Primary server is healthy
                    _useBackup = false;
                }
                else
                {
                    // Primary server is down, use backup
                    _useBackup = true;
                }
            }
            catch
            {
                // Exception indicates primary server is down
                _useBackup = true;
            }
        }

        public string GetActiveServerPath()
        {
            return !_useBackup ? _nfsOptions.MountDir : _nfsOptions.MountDirBackup;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}