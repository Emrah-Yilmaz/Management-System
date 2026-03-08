namespace ManagementSystem.Domain.Configurations
{
    public class FileUploadSettings
    {
        public string StoragePath { get; set; } = "C:\\files";
        public int MaxFileSizeInMB { get; set; } = 8;
        public string[] AllowedExtensions { get; set; } = new[] { ".pdf", ".jpg", ".jpeg", ".png", ".doc", ".docx", ".xls", ".xlsx" };
    }
}
