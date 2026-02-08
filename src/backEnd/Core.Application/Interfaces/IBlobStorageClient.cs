using System.IO;

namespace Core.Application.Interfaces;

public interface IBlobStorageClient
{
    /// <summary>
    /// Uploads content to blob storage and returns the public URL.
    /// </summary>
    /// <param name="content">Content stream to upload.</param>
    /// <param name="fileName">Original file name.</param>
    /// <param name="contentType">Optional content type.</param>
    /// <param name="prefix">Optional virtual folder prefix.</param>
    /// <returns>Blob URL.</returns>
    Task<string> UploadAsync(Stream content, string fileName, string? contentType, string? prefix = null);
}