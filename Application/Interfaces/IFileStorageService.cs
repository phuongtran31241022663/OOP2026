using System.IO;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    /// <summary>
    /// Interface for file storage service.
    /// Implementation should be in Infrastructure/ExternalServices/
    /// Giao diện cho dịch vụ lưu trữ tệp. Triển khai nên được đặt trong Infrastructure.
    /// </summary>
    public interface IFileStorageService
    {
        Task<string> UploadFileAsync(Stream fileStream, string fileName, string contentType);
        Task<Stream> DownloadFileAsync(string filePath);
        Task DeleteFileAsync(string filePath);
        Task<bool> FileExistsAsync(string filePath);
    }
}
