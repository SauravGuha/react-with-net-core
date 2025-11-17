

namespace Application.ViewModels
{
    public class ResetPasswordViewModel
    {
        public required string ResetCode { get; set; }

        public required string EmailId { get; set; }

        public required string NewPassword { get; set; }
    }
}