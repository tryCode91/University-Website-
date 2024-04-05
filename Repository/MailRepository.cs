using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using Teaching.Helper;
using Teaching.Interface;
using Teaching.Models;
using Teaching.ViewModels;

namespace Teaching.Repository
{
    public class MailRepository : IMailRepository
    {
        private readonly EmailSettings _emailSettings;

        public MailRepository(IOptions<EmailSettings> options)
        {
            _emailSettings = options.Value;
        }

        public async Task SendEmailAsync(MailViewModel mailViewModel, bool isAuthorized)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(mailViewModel.From, mailViewModel.From));
            message.To.Add(new MailboxAddress("", _emailSettings.Email));
            message.Subject = mailViewModel.Subject;

            message.Body = new TextPart("html") 
            { 
                Text = GetHtmlContent(mailViewModel, isAuthorized)
            };

            using (SmtpClient client = new SmtpClient())
            {
                client.Connect(_emailSettings.Host, _emailSettings.Port, SecureSocketOptions.StartTls);
                client.Authenticate(_emailSettings.Email, _emailSettings.Password);

                await client.SendAsync(message);            
                client.Disconnect(true);
            
            }
        }

        private string GetHtmlContent(MailViewModel mailViewModel, bool isAuthorized)
        {
            string response = "<div style=\"width:100%;background-color:lightblue;text-align:center;margin:10px\">";
            if (isAuthorized)
            {
                response += "<h1>A message was sent from " + mailViewModel.From + " (Authorized user)<h1>";
            } 
            else
            {
                response += "<h1>A message was sent from " + mailViewModel.From + " (Unauthorized user)<h1>";
            }

            response += "<hr/>";
            response += "<p>" + mailViewModel.Body + "</p>";
            response += "</div>";
            return response;
        }
    }
}
