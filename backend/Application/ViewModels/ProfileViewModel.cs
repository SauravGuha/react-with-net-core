

namespace Application.ViewModels
{
    public class ProfileViewModel
    {
        public string? Bio { get; set; }
        public string? DisplayName { get; set; }

        public required string Email { get; set; }

        public required string Id { get; set; }

        public List<PhotoViewModel> Photos { get; set; } = new();
    }

    public class PhotoViewModel
    {
        public required string PublicId { get; set; }

        public required string Url { get; set; }
    }
}