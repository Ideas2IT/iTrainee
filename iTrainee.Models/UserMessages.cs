using System;
using System.Collections.Generic;
using System.Text;

namespace iTrainee.Models
{
    public class UserMessages : Base
    {
        public int Id { get; set; }

        public int ToId { get; set; }

        public int MessageId { get; set; }

        public string Comments { get; set; }

        public Boolean IsRead { get; set; }
    }
}
