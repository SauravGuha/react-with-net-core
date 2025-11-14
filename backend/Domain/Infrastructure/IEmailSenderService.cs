
namespace Domain.Infrastructure
{
    public interface IEmailSenderService
    {
        Task SendConfirmationEmail(string toEmailId, string userName);
    }
}