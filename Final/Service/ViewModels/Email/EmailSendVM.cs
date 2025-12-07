using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Service.ViewModels.Email
{
    public class EmailSendVM
    {
        public string To { get; set; }
        public string Body { get; set; }
        public string Subject { get; set; }
        public List<IFormFile> Files { get; set; }
    }
}
