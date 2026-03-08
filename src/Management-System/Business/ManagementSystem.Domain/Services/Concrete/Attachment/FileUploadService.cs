using ManagementSystem.Domain.Configurations;
using ManagementSystem.Domain.Services.Abstract.Attachment;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace ManagementSystem.Domain.Services.Concrete.Attachment
{
    public class FileUploadService : IFileUploadService
    {
        private readonly FileUploadSettings _settings;

        public FileUploadService(IOptions<FileUploadSettings> options)
        {
            _settings = options.Value;
        }

        public async Task<(bool IsSuccess, string? FilePath, string? ErrorMessage)> UploadFileAsync(IFormFile file, CancellationToken cancellationToken = default)
        {
            if (file == null || file.Length == 0)
                return (false, null, "No file provided.");

            // 1. Check size limit
            long maxBytes = _settings.MaxFileSizeInMB * 1024 * 1024;
            if (file.Length > maxBytes)
                return (false, null, $"File size exceeds the maximum limit of {_settings.MaxFileSizeInMB} MB.");

            // 2. Check extension
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!_settings.AllowedExtensions.Contains(extension))
                return (false, null, "File extension is not allowed.");

            // 3. Ensure directory exists
            if (!Directory.Exists(_settings.StoragePath))
            {
                Directory.CreateDirectory(_settings.StoragePath);
            }

            // 4. Generate unique file name to avoid collisions
            var uniqueFileName = $"{Guid.NewGuid()}{extension}";
            var fullPath = Path.Combine(_settings.StoragePath, uniqueFileName);

            // 5. Save the file
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream, cancellationToken);
            }

            return (true, fullPath, null);
        }

        public async Task<Tuple<Stream, string, string>?> DownloadFileAsync(string filePath, CancellationToken cancellationToken = default)
        {
            if (!File.Exists(filePath))
                return null;

            var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            var fileName = Path.GetFileName(filePath);
            
            // Basic mapping
            var ext = Path.GetExtension(filePath).ToLowerInvariant();
            string contentType = ext switch
            {
                ".pdf" => "application/pdf",
                ".jpg" => "image/jpeg",
                ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".doc" => "application/msword",
                ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                ".xls" => "application/vnd.ms-excel",
                ".xlsx" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                _ => "application/octet-stream"
            };

            return new Tuple<Stream, string, string>(stream, contentType, fileName);
        }
    }
}
