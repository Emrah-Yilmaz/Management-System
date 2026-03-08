using Microsoft.AspNetCore.Http;

namespace ManagementSystem.Domain.Services.Abstract.Attachment
{
    public interface IFileUploadService
    {
        Task<(bool IsSuccess, string? FilePath, string? ErrorMessage)> UploadFileAsync(IFormFile file, CancellationToken cancellationToken = default);
        Task<Tuple<Stream, string, string>?> DownloadFileAsync(string filePath, CancellationToken cancellationToken = default);
    }
}
