

namespace Application.ViewModels
{
    public class CommentCommandViewModel
    {
        public required string Body { get; set; }

        public required Guid ActivityId { get; set; }
    }
}