
namespace Domain.Infrastructure
{
    public interface IEmailSenderService
    {
        Task SendConfirmationEmail(string userId, string toEmailId, string userName, string confirmationCode);
    }
}