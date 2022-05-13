using System;
using System.ComponentModel.DataAnnotations;

namespace iTrainee.Models
{
    public class User : Base
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DOB { get; set; }

        public int RoleId { get; set; }

        public string Qualification { get; set; }

        public string UserName { get; set; }

        [Required(ErrorMessage = "Please Enter Your Password")]
        [DataType(DataType.Password)]
        [StringLength(18, ErrorMessage = "The password must be atleast 3 characters long", MinimumLength = 3)]
        public string Password { get; set; }

        public Boolean IsAdmin { get; set; }

        public Boolean IsMentor { get; set; }

        public Boolean IsTrainee { get; set; }

        public string RoleName { get; set; } 
    }
}