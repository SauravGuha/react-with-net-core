

using Domain.Infrastructure;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Infrastructure
{
    public class EmailSenderService : IEmailSenderService
    {
        private string? sendGridKey;
        private string? applicationUrl;

        public EmailSenderService(IConfiguration configuration)
        {
            sendGridKey = configuration.GetConnectionString("SendGridKey");
            applicationUrl = configuration.GetSection("ApplicationUrl")?.Value;
            if (sendGridKey == null)
            {
                throw new ArgumentNullException("SendGridKey missing from configuration");
            }
            if (applicationUrl == null)
            {
                throw new ArgumentNullException("ApplicationUrl missing from configuration");
            }
        }
        public async Task SendConfirmationEmail(string toEmailId, string userName)
        {
            var client = new SendGridClient(sendGridKey);
            var from = new EmailAddress("sauravguha0112@gmail.com");
            var subject = "Email confirmation";
            var to = new EmailAddress(toEmailId, userName);
            var htmlContent = $"Click here to confirm email address <a href=\"${applicationUrl}/activityaccount/confirmemailaddress\"/>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, "Please click anywhere to confirm", htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}