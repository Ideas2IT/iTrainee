﻿using System;

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

        public string Password { get; set; }

        public Boolean IsAdmin { get; set; }

        public Boolean IsMentor { get; set; }

        public Boolean IsTrainee { get; set; }
    }
}