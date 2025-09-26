

namespace Application.ViewModels
{
    public class ActivityViewModel
    {
        public required string Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required string EventDate { get; set; }
        public required string Category { get; set; }
        public required bool IsCancelled { get; set; }

        //location properties
        public required string City { get; set; }
        public required string Venue { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}