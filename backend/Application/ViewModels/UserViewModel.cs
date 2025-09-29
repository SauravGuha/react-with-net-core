

namespace Application.ViewModels
{
    public class UserViewModel
    {
        public string? Bio { get; set; }
        public string? DisplayName { get; set; }

        public required string Email { get; set; }

        public string? ImageUrl { get; set; }

        public required string Id { get; set; }
    }
}