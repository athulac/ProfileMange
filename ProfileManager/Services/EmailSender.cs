using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net.Mail;
using System.Net;
using System.Text;

namespace ProfileManager.Services
{
    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            //// Set up SMTP client
            //SmtpClient client = new SmtpClient("smtp.ethereal.email", 587);
            //client.EnableSsl = true;
            //client.UseDefaultCredentials = false;
            //client.Credentials = new NetworkCredential("abby.wehner53@ethereal.email", "djgwPz8DPss78aakBv");

            //// Create email message
            //MailMessage mailMessage = new MailMessage();
            //mailMessage.From = new MailAddress("abby.wehner53@ethereal.email");
            //mailMessage.To.Add(email);
            //mailMessage.Subject = subject;
            //mailMessage.IsBodyHtml = true;
            //StringBuilder mailBody = new StringBuilder();
            //mailBody.AppendFormat("<h1>User Registered</h1>");
            //mailBody.AppendFormat("<br />");
            //mailBody.AppendFormat("<p>Thank you For Registering account</p>");
            //mailMessage.Body = mailBody.ToString();

            //// Send email
            //client.Send(mailMessage);
        }
    }
}
