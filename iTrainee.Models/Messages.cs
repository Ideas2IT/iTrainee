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

        [NotMapped]
        public string[] UnselectedTraineeIds { get; set; }

        [NotMapped]
        public string TraineesIdsString { get; set; }

        [NotMapped]
        public string SelectedTraineeIdsString { get; set; }

        [NotMapped]
        public string UnselectedTraineeIdsString { get; set; }
    }
}
