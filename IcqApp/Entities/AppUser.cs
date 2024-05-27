using System.ComponentModel.DataAnnotations;

namespace IcqApp.Entities
{
    public class AppUser
    {
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public DateOnly DateOfBirth { get; set; }

        public string KnownAs { get; set; }

        public DateTime Created { get; set; } = DateTime.UtcNow;

        public DateTime LastActive { get; set; } = DateTime.UtcNow;

        public List<Photo> Photos { get; set; } = new List<Photo>();

        public List<UserFriendship> FriendByUsers { get; set; }

        public List<UserFriendship> FriendUsers { get; set; }

    }
}
