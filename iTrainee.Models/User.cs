﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iTrainee.Models
{
    public class User : Base
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter first name")]
        [RegularExpression(@"^([a-zA-Z .]*)$", ErrorMessage = "Invalid format")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter last name")]
        [RegularExpression(@"^([a-zA-Z .]*)$", ErrorMessage = "Invalid format")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter date of birth")]
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }

        public int RoleId { get; set; }

        public string Qualification { get; set; }

        public string UserName { get; set; }

        [Required(ErrorMessage = "Please Enter Your Password")]
        [DataType(DataType.Password)]
        [StringLength(18, ErrorMessage = "The password must be atleast 8 characters long", MinimumLength = 8)]
        public string Password { get; set; }

        public Boolean IsAdmin { get; set; }

        public Boolean IsMentor { get; set; }

        public Boolean IsTrainee { get; set; }

        public string RoleName { get; set; } 

        public string Token { get; set; }

        public int UnreadMessagesCount { get; set; }

        [NotMapped]
        public string[] SelectedTraineeIds { get; set; }

    }
}