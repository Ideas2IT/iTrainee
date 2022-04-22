using System;

namespace iTrainee.Models
{
    public class Base
    {
        public Boolean IsActive { get; set; }

        public string InsertedBy { get; set; }

        public DateTime InsertedOn { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime UpdatedOn { get; set; }

    }
}
