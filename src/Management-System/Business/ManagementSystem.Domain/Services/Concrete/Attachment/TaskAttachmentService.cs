using ManagementSystem.Domain.Entities;
using ManagementSystem.Domain.Persistence.Attachment;
using ManagementSystem.Domain.Services.Abstract.Attachment;
using Microsoft.AspNetCore.Http;

namespace ManagementSystem.Domain.Services.Concrete.Attachment
{
    public class TaskAttachmentService : ITaskAttachmentService
    {
        private readonly ITaskAttachmentRepository _attachmentRepository;
        private readonly IFileUploadService _fileUploadService;

        public TaskAttachmentService(
            ITaskAttachmentRepository attachmentRepository, 
            IFileUploadService fileUploadService)
        {
            _attachmentRepository = attachmentRepository;
            _fileUploadService = fileUploadService;
        }

        public async Task<bool> DeleteAttachmentAsync(int attachmentId, CancellationToken cancellationToken = default)
        {
            var attachment = await _attachmentRepository.GetByIdAsync(attachmentId, false, cancellationToken);
            if (attachment == null) return false;

            // Optional: delete actual file from the disk as well
            if (File.Exists(attachment.FilePath))
            {
                File.Delete(attachment.FilePath);
            }

            return (await _attachmentRepository.DeleteAsync(attachmentId, cancellationToken)) > 0;
        }

        public async Task<TaskAttachment?> GetAttachmentByIdAsync(int attachmentId, CancellationToken cancellationToken = default)
        {
            return await _attachmentRepository.GetByIdAsync(attachmentId, true, cancellationToken);
        }

        public async Task<List<TaskAttachment>> GetAttachmentsByTaskIdAsync(int taskId, CancellationToken cancellationToken = default)
        {
            var result = await _attachmentRepository.GetListAsync(
                predicate: a => a.WorkTaskId == taskId,
                noTracking: true,
                includes: null); // You may want to include user navigation here if needed

            return result?.ToList() ?? new List<TaskAttachment>();
        }

        public async Task<TaskAttachment?> UploadAttachmentAsync(int taskId, int userId, IFormFile file, CancellationToken cancellationToken = default)
        {
            var (isSuccess, filePath, errorMessage) = await _fileUploadService.UploadFileAsync(file, cancellationToken);
            
            if (!isSuccess || string.IsNullOrEmpty(filePath))
            {
                throw new Exception($"File upload failed: {errorMessage}");
            }

            var attachment = new TaskAttachment
            {
                WorkTaskId = taskId,
                UserId = userId,
                FileName = file.FileName,
                FilePath = filePath,
                ContentType = file.ContentType,
                FileSize = file.Length
            };

            await _attachmentRepository.AddAsync(attachment, cancellationToken);
            return attachment;
        }
    }
}
