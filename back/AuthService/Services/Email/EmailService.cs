using System.Net.Mail;
using AuthService.Models;
using System.Xml.Linq;
using Resend;
using AuthService.Services.RazorViewToStringRenderer;


namespace AuthService.Services.Email
{
    public class EmailService
    {

        private readonly IResend _resend;

        private readonly IRazorViewToStringRenderer _razorRenderer;

        public EmailService(IResend resend, IRazorViewToStringRenderer razorRenderer)
        {
            _resend = resend;
            _razorRenderer = razorRenderer;
        }


        public async Task Execute(string email, string name)
        {

            var emailModel = new WelcomeEmailModel
            {
                Name = name,
                Email = email,
                Message = "Your account has been successfully created. Start exploring our platform with movies today!",
                ActionUrl = "https://localhost:30000/dashboard"
            };
            var htmlBody = await _razorRenderer.RenderViewToStringAsync("~/Views/Emails/Welcome.cshtml", emailModel);

            var message = new EmailMessage()
            {
                From = "Acme <onboarding@resend.dev>",
                To = email,
                Subject = "Email Verification",
                HtmlBody = htmlBody
            };

            await _resend.EmailSendAsync(message);
            Console.WriteLine("EMAIL SENT");
        }
    }
}
