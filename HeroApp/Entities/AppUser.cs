﻿using System.ComponentModel.DataAnnotations;

namespace HeroApp.Entities
{
    public class AppUser
    {
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }
    }
}
