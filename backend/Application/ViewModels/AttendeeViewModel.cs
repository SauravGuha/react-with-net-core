

namespace Application.ViewModels
{
    public class AttendeeViewModel
    {
        public required bool IsHost { get; set; }
        public required bool IsAttending { get; set; }

        public required UserViewModel User { get; set; }
    }
}