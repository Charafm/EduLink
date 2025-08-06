namespace SchoolSaas.Application.Common.Interfaces
{
    public interface IStorage : IDisposable
    {
        Task<string> SaveFileAsync(byte[] data, string absolutePath, string fileName);
        Task<string> SaveFileAsync(Stream stream, string absolutePath, string fileName);

        Task<bool> DeleteFileAsync(string fileAbsolutePath);

        Task<byte[]?> GetFileAsBytesAsync(string fileAbsolutePath);
        Task<Stream?> GetFileAsync(string fileAbsolutePath);

        Task<List<string>> GetFilesAsync(string absolutePath, string folderRelativePath);

        Task<string> CreateFolderAsync(string absolutePath, string folderRelativePath);

        Task<bool> DeleteFolderAsync(string absolutePath, string folderRelativePath);

        Task<List<string>> GetFoldersAsync(string absolutePath, string folderRelativePath);
    }
}