using Microsoft.AspNetCore.Http;

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

        public async Task<string> UploadAsync(IFormFile file, string folderName)
        {
            if (file == null || file.Length == 0)
                throw new InvalidOperationException("File is empty");

            if (file.Length > maxSize)
                throw new InvalidOperationException($"File size exceeds {maxSize / (1024 * 1024)} MB");

            var extention = Path.GetExtension(file.FileName);
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
            string fileName = $"{Guid.NewGuid().ToString()}{extention}";
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", folderName);
            string filePath = Path.Combine(folderPath, fileName);

            using FileStream stream = new FileStream(filePath, FileMode.OpenOrCreate);
            await file.CopyToAsync(stream);
            return filePath;
        }
    }
}