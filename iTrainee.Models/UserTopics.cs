using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace iTrainee.Models
{
    public class UserTopics : Base
    {
        public int Id { get; set; }

        [NotMapped]
        public string Username { get; set; }

        [NotMapped]
        public IEnumerable<User> TraineeList { get; set; }

        [NotMapped]
        public IEnumerable<Topics> TopicsList { get; set; }

        [NotMapped]
        public IEnumerable<SubTopics> SubTopicsList { get; set; }

        [NotMapped]
        public string TopicName { get; set; }

        [NotMapped]
        public string SubTopicName { get; set; }

        [NotMapped]
        public string[] SelectedTopicsList { get; set; }

        [NotMapped]
        public string[] SelectedSubTopicsList { get; set; }
    }
}
