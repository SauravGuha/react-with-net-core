

namespace Application.ViewModels
{
    public class AttendeeViewModel
    {
        public string UserId { get; set; }

        public Guid ActivityId { get; set; }

        public required bool IsHost { get; set; }
        public required bool IsAttending { get; set; }
    }
}