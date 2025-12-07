using Service.ViewModels.Email;

namespace Service.Service.Interfaces
{
    public interface IEmailService
    {
        void SendEmailAsync(EmailSendVM vm);
    }
}
