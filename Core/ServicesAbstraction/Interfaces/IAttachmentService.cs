using Microsoft.AspNetCore.Http;

namespace ServicesAbstraction.Interfaces
{
    public interface IAttachmentService
    {
        Task<string> UploadAsync(IFormFile file, string folderName);

        bool Delete(string path);
    }
}