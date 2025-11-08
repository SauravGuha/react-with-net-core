

using System.ComponentModel.DataAnnotations;

namespace Application.ViewModels
{
    public class RegistrationViewModel
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
        public string? DisplayName { get; set; }
        public string? Bio { get; set; }

        public string? ImageUrl { get; set; }
    }
}