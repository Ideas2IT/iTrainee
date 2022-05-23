using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace iTrainee.Models
{
    public class Messages : Base
    {
        public int Id { get; set; }

        public int FromId { get; set; }

        public string Message { get; set; }

        [NotMapped]
        public IEnumerable<User> TraineeList { get; set; }

        [NotMapped]
        public string[] SelectedTraineeIds { get; set; }
    }
}
