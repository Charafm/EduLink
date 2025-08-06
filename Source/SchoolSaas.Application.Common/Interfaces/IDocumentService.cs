using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Application.Common.Interfaces
{
    public interface IDocumentService
    {
        Task<string> UploadAsync(Stream stream, string fileName, string contentType, CancellationToken ct = default);
        Task<Stream> DownloadAsync(string blobName, CancellationToken ct = default);
        Task DeleteAsync(string blobName, CancellationToken ct = default);
    }
}
