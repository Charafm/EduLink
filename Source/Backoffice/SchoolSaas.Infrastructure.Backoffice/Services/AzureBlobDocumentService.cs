using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;
using SchoolSaas.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolSaas.Infrastructure.Backoffice.Services
{
    public  class AzureBlobDocumentService : IDocumentService
    {
        private readonly BlobContainerClient _container;

        public AzureBlobDocumentService(IConfiguration config)
        {
            var conn = config.GetConnectionString("AzureBlobStorage");
            var containerName = config["BlobContainerName"];
            _container = new BlobContainerClient(conn, containerName);
        }

        public async Task<string> UploadAsync(Stream stream, string fileName, string contentType, CancellationToken ct = default)
        {
            var blob = _container.GetBlobClient(fileName);
            var headers = new BlobHttpHeaders { ContentType = contentType };
            await blob.UploadAsync(stream, new BlobUploadOptions { HttpHeaders = headers }, ct);
            return blob.Name;
        }

        public async Task<Stream> DownloadAsync(string blobName, CancellationToken ct = default) =>
            (await _container.GetBlobClient(blobName).DownloadStreamingAsync(cancellationToken: ct)).Value.Content;

        public async Task DeleteAsync(string blobName, CancellationToken ct = default) =>
            await _container.GetBlobClient(blobName).DeleteIfExistsAsync(cancellationToken: ct);
    }
}
