

using Microsoft.AspNetCore.Identity;

namespace Domain.Models
{
    public class User : IdentityUser
    {
        public string? DisplayName { get; set; }
        public string? Bio { get; set; }

        public string? ImageUrl { get; set; }

        public virtual ICollection<Attendees> Attendees { get; set; } = [];

        public virtual ICollection<Photo> Photos { get; set; } = [];

        public virtual ICollection<UserFollowing> Followings { get; set; } = [];

        public virtual ICollection<UserFollowing> Followers { get; set; } = [];
    }
}