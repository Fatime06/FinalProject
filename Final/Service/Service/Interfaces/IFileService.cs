using Microsoft.AspNetCore.Http;

namespace Service.Service.Interfaces
{
    public interface IFileService
    {
        Task<string> UploadAsync(IFormFile file, string folder);
        void Delete(string fileName, string folder);
    }
}
