using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Core.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Service;

public class AzureBlobStorageClient : IBlobStorageClient
{
    private readonly BlobContainerClient _containerClient;
    private readonly ILogger<AzureBlobStorageClient> _logger;

    public AzureBlobStorageClient(ILogger<AzureBlobStorageClient> logger)
    {
        _logger = logger;

        string connectionString = Environment.GetEnvironmentVariable("AZURE_STORAGE_CONNECTION_STRING")
            ?? throw new InvalidOperationException("Environment variable 'AZURE_STORAGE_CONNECTION_STRING' is not set.");

        string containerName = Environment.GetEnvironmentVariable("AZURE_STORAGE_CONTAINER_EVIDENCE") ?? "evidencias-articulos";

        BlobServiceClient serviceClient = new BlobServiceClient(connectionString);
        _containerClient = serviceClient.GetBlobContainerClient(containerName);
    }

    public async Task<string> UploadAsync(Stream content, string fileName, string? contentType, string? prefix = null)
    {
        if (content == null)
        {
            throw new ArgumentNullException(nameof(content));
        }

        string sanitizedFileName = BuildSafeFileName(fileName);
        string blobName = string.IsNullOrWhiteSpace(prefix)
            ? sanitizedFileName
            : $"{prefix.Trim().Replace(" ", "_")}/{sanitizedFileName}";

        await _containerClient.CreateIfNotExistsAsync(PublicAccessType.None);

        BlobClient blobClient = _containerClient.GetBlobClient(blobName);

        if (content.CanSeek)
        {
            content.Position = 0;
        }

        BlobUploadOptions options = new BlobUploadOptions();
        if (!string.IsNullOrWhiteSpace(contentType))
        {
            options.HttpHeaders = new BlobHttpHeaders { ContentType = contentType };
        }

        await blobClient.UploadAsync(content, options);
        _logger.LogInformation("Uploaded evidence blob {BlobName}", blobName);

        return blobClient.Uri.ToString();
    }

    private static string BuildSafeFileName(string fileName)
    {
        string extension = Path.GetExtension(fileName);
        string safeExtension = string.IsNullOrWhiteSpace(extension) ? string.Empty : extension.ToLowerInvariant();

        return $"{DateTime.UtcNow:yyyyMMddHHmmss}_{Guid.NewGuid():N}{safeExtension}";
    }
}