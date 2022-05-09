using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace iTrainee.Models
{
    public class Batch : Base
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [NotMapped]
        public IEnumerable<User> MentorList { get; set; }

        [NotMapped]
        public IEnumerable<User> TraineeList { get; set; }

        [NotMapped]
        public IEnumerable<Stream> StreamList { get; set; }

        [NotMapped]
        public string[] SelectedMentorIds { get; set; }

        [NotMapped]
        public string[] SelectedTraineeIds { get; set; }

        [NotMapped]
        public string[] SelectedStreamIds { get; set; }

        [NotMapped]
        public string StringUserIds { get; set; }

        [NotMapped]
        public string StringStreamIds { get; set; }
    }
}
