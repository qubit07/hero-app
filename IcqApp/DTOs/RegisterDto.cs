﻿using System.ComponentModel.DataAnnotations;

namespace IcqApp.DTOs
{
    public class RegisterDto
    {
        [Required]
        [MinLength(3)]
        public string Username { get; set; }

        [Required]
        [MinLength(3)]
        public string KnownAs { get; set; }

        [Required]
        public DateOnly? DateOfBirth { get; set; }

        [Required]
        [MinLength(8)]
        public string Password { get; set; }


    }
}
