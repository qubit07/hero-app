using Microsoft.AspNetCore.Identity;

namespace IcqApp.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public DateOnly DateOfBirth { get; set; }

        public string KnownAs { get; set; }

        public DateTime Created { get; set; } = DateTime.UtcNow;

        public DateTime LastActive { get; set; } = DateTime.UtcNow;

        public List<Photo> Photos { get; set; } = new List<Photo>();

        public List<UserFriendship> FriendByUsers { get; set; }

        public List<UserFriendship> FriendUsers { get; set; }

        public List<Message> MessagesSend { get; set; }
        public List<Message> MessagesReceived { get; set; }

        public ICollection<AppUserRole> UserRoles { get; set; }

    }
}
