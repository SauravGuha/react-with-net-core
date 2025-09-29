

using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class Attendees : BaseModel
    {
        public string UserId { get; set; }
        public Guid ActivityId { get; set; }
        public virtual User User { get; set; } = null!;
        public virtual Activity Activity { get; set; } = null!;
        public required bool IsHost { get; set; }
        public required bool IsAttending { get; set; }
    }
}