﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace L01SampleAuth.Models
{
    public class ApplicationUser: IdentityUser
    {
        [Display(Name = "First Name")]
        public string FirstName { get; set; } 
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

    }
}
