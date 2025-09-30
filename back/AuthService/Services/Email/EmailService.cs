using System.Net.Mail;
using Resend;


namespace AuthService.Services.Email
{
    public class EmailService
    {

        private readonly IResend _resend;


        public EmailService(IResend resend)
        {
            _resend = resend;
        }


        public async Task Execute(string email)
        {
            var message = new EmailMessage();
            message.From = "Acme <onboarding@resend.dev>";
            message.To.Add("t");
            message.Subject = "Hii";
            message.HtmlBody = "<strong>sdadas</strong>";
            await _resend.EmailSendAsync(message);
            Console.WriteLine("EMAIL SENT");
        }
    }
}
