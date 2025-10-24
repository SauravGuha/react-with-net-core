

namespace Domain.Models
{
    public class Comment : BaseModel
    {
        public required string Body { get; set; }

        public required Guid ActivityId { get; set; }
        public virtual Activity Activity { get; set; } = null!;

        public required string UserId { get; set; }
        public virtual User User { get; set; } = null!;
    }
}