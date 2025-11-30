using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Service.Service.Interfaces;

namespace Service.Service
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _env;

        public FileService(IWebHostEnvironment env)
        {
            _env = env;
        }
        public void Delete(string fileName, string folder)
        {
            string uploadPath = Path.Combine(_env.WebRootPath, folder);
            string filePath = Path.Combine(uploadPath, fileName);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        public async Task<string> UploadAsync(IFormFile file, string folder)
        {
            string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string uploadPath = Path.Combine(_env.WebRootPath, folder);

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            string filePath = Path.Combine(uploadPath, uniqueFileName);

            using (FileStream stream = new(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return uniqueFileName;
        }
    }
}
