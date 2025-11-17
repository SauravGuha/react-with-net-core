

using Domain.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Infrastructure
{
    public class EmailSenderService : IEmailSenderService
    {
        private readonly ILogger<EmailSenderService> logger;
        private string? sendGridKey;
        private string? applicationUrl;

        public EmailSenderService(IConfiguration configuration, ILogger<EmailSenderService> logger)
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

            this.logger = logger;
        }
        public async Task SendConfirmationEmail(string userId, string toEmailId, string userName, string confirmationCode)
        {
            var client = new SendGridClient(sendGridKey);
            var from = new EmailAddress("sauravguha0112@gmail.com");
            var subject = "Email confirmation";
            var to = new EmailAddress(toEmailId, userName);
            var htmlContent = $@"
<div style=""font-family: Arial, sans-serif; padding: 20px; line-height: 1.6; color: #333;"">

    <h2 style=""margin-bottom: 10px; color: #444;"">Email Confirmation</h2>

    <p>Hi {userName},</p>

    <p>
        Thank you for registering. Please confirm your email address by clicking the button below.
    </p>

    <a href=""{applicationUrl}/api/activityaccount/EmailConfirmation?userId={userId}&code={confirmationCode}"" 
       style=""display:inline-block; padding:10px 20px; background-color:#4CAF50; 
              color:white; text-decoration:none; border-radius:5px; margin:20px 0;"">
        Confirm Email
    </a>

    <p>
        If the button above doesn't work, copy and paste the link into your browser:
        <br />
        {applicationUrl}/api/activityaccount/EmailConfirmation?userId={userId}&code={confirmationCode}
    </p>

    <p style=""margin-top:30px; font-size:12px; color:#777;"">
        This link is valid for 4 hours. If you did not request this, please ignore this email.
    </p>

</div>";

            var msg = MailHelper.CreateSingleEmail(from, to, subject, "Please click anywhere to confirm", htmlContent);
            var response = await client.SendEmailAsync(msg);
            if (!response.IsSuccessStatusCode)
            {
                var errorBody = await response.DeserializeResponseBodyAsync(response.Body);
                logger.LogError(string.Join("\n", errorBody.Keys));
            }
        }
    }
}