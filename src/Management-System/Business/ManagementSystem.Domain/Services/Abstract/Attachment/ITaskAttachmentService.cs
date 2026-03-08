using Microsoft.AspNetCore.Http;
using ManagementSystem.Domain.Entities;

namespace ManagementSystem.Domain.Services.Abstract.Attachment
{
    public interface ITaskAttachmentService
    {
        Task<List<TaskAttachment>> GetAttachmentsByTaskIdAsync(int taskId, CancellationToken cancellationToken = default);
        Task<TaskAttachment?> GetAttachmentByIdAsync(int attachmentId, CancellationToken cancellationToken = default);
        Task<TaskAttachment?> UploadAttachmentAsync(int taskId, int userId, IFormFile file, CancellationToken cancellationToken = default);
        Task<bool> DeleteAttachmentAsync(int attachmentId, CancellationToken cancellationToken = default);
    }
}
