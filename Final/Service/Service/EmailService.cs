using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using Service.Service.Interfaces;
using Service.ViewModels.Email;


namespace Service.Service
{
    public class EmailService : IEmailService
    {
        public void SendEmailAsync(EmailSendVM vm)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("fatimafa@code.edu.az"));
            email.To.Add(MailboxAddress.Parse(vm.To));
            email.Subject = vm.Subject;
            email.Body = new TextPart(TextFormat.Html) { Text = vm.Body };

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("fatimafa@code.edu.az", "zkhg yfph idix mphr");
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
