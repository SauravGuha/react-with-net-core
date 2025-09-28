

namespace Domain.Models
{
    public class User : BaseModel
    {
        public required string Email { get; set; }

        public required string UserName { get; set; }

        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public string? PhoneNumber { get; set; }

        public string? ImageUrl { get; set; }
    }
}