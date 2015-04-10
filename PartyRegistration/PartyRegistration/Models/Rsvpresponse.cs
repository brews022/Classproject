using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PartyRegistration.Models
{
    public class Rsvpresponse
    {
        [Required(ErrorMessage ="Please enter your name")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Please enter your email")]
        [RegularExpression(@"^\S+@\S+$",
            ErrorMessage = "The email address format isn't valid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter your phone number")]
        public string Phone { get; set; }
        
        [Required(ErrorMessage = "Please enter your favorite game")]
        public string FavoriteGame { get; set; }
        
        [Required(ErrorMessage = "Please specify whether you will attend")]
        public bool? WillAttend { get; set; }
       
    }
}