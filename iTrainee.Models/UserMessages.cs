﻿using System;

namespace iTrainee.Models
{
    public class UserMessages : Base
    {
        public string ToId { get; set; }

        public int MessageId { get; set; }

        public string Comments { get; set; }

        public Boolean IsRead { get; set; }

        public string Sender { get; set; }

        public string Receiver { get; set; }
        public string Message { get; set; }
    }
}
