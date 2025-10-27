

namespace Application.ViewModels
{
    public class CommentViewModel
    {
        public required Guid Id { get; set; }

        public required string CreatedAt { get; set; }

        public required string Body { get; set; }

        public required Guid ActivityId { get; set; }

        public required string UserId { get; set; }

        public string? DisplayName { get; set; }

        public string? ImageUrl { get; set; }
    }
}