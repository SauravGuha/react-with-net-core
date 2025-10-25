

namespace Application.ViewModels
{
    public class CommentViewModel
    {
        public required Guid Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public required string Body { get; set; }

        public required Guid ActivityId { get; set; }

        public string? DisplayName { get; set; }

        public string? ImageUrl { get; set; }
    }
}