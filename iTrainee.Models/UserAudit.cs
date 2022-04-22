using System;
using System.Collections.Generic;
using System.Text;

namespace iTrainee.Models
{
    public class UserAudit : Base
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public DateTime Date { get; set; }

        public DateTime SignIn { get; set; }

        public DateTime SignOut { get; set; }
    }
}
