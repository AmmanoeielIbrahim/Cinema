using Microsoft.AspNetCore.Http;
using ServicesAbstraction.Interfaces;
using System.Text;

namespace Services.Implementation
{
    public class AttachmentService : IAttachmentService
    {
        private readonly string[] allowedExtentions = [".png", ".jpg", ".jpeg"];
        private readonly int maxSize = 2 * 1024 * 1024;

        public bool Delete(string path)
        {
            if (!File.Exists(path)) return false;
            File.Delete(path);
            return true;
        }

        public string Upload(IFormFile file, string folderName)
        {
            var extention = Path.GetExtension(file.Name);
            if (!allowedExtentions.Contains(extention))
            {
                StringBuilder allowed = new StringBuilder();
                foreach (var item in allowedExtentions)
                {
                    if (allowed.Length > 0) allowed.Append(" , ");
                    allowed.Append(item);
                }
                throw new InvalidOperationException($"Not allwoed {extention}," +
                    $" just allowed one of them {allowed.ToString()}");
            }
            if (file.Length == 0 || file.Length > maxSize)
                throw new InvalidOperationException($"Your File size is {file.Length / (1024 * 1024)}M, max Size allwoed is {maxSize / (1024 * 1024)}M");

            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwrooot", folderName, "_", new Guid().ToString(), extention);

            using FileStream stream = new FileStream(filePath, FileMode.OpenOrCreate);
            file.CopyTo(stream);
            return filePath;
        }
    }
}