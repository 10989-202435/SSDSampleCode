﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace L04Cryptography.Models
{
    public class Password
    {
        [Key]
        public int Id { get; set; }
        [ReadOnly(true)]
        public string? UserId { get; set; }
        [Required]
        public string PlainTextPassword { get; set; }
        [ReadOnly(true)]
        public string? HashedPassword { get; set; }
        [ReadOnly(true)]
        public string? Salt { get; set; }
        [ReadOnly(true)]
        public string? HashedSaltedPassword { get; set; }
        [ReadOnly(true)]
        public string? BcryptPassword { get; set; } 
    }
}
