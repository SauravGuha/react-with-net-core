
namespace Domain.Models
{
    public class Activity : BaseModel
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required DateTime EventDate { get; set; }
        public required string Category { get; set; }
        public required bool IsCancelled { get; set; }

        //location properties
        public required string City { get; set; }
        public required string Venue { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public virtual ICollection<Attendees> Attendees { get; set; } = [];

        public virtual ICollection<Comment> Comments { get; set; } = [];
    }
}