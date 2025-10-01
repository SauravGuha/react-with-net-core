
namespace Application.ViewModels
{
    public class AttendeeCommandViewModel
    {
        public required bool IsHost { get; set; }
        public required bool IsAttending { get; set; }

        public required string UserId { get; set; }

        public required string ActivityId { get; set; }
    }
}