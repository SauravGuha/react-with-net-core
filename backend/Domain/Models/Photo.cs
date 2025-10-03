

namespace Domain.Models
{
    public class Photo : BaseModel
    {
        public required string PublicId { get; set; }

        public required string Url { get; set; }

        public required string UserId { get; set; }

        public virtual User User { get; set; } = null!;
    }
}