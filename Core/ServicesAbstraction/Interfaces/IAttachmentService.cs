using Microsoft.AspNetCore.Http;

namespace ServicesAbstraction.Interfaces
{
    public interface IAttachmentService
    {
        string Upload(IFormFile file, string folderName);

        bool Delete(string path);
    }
}