using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace iTrainee.Models
{
    public class UserTopics : Base
    {
        public string id { get; set; }
        public string parent { get; set; }
        public string text { get; set; }

        [NotMapped]
        public List<User> TraineeList { get; set; }
        [NotMapped]
        public List<Topics> TopicsList { get; set; }

        [NotMapped]
        public string[] SelectedTraineeList { get; set; }
        [NotMapped]
        public string[] SelectedTopicList { get; set; }

        [NotMapped]
        public string[] SelectedSubTopicList { get; set; }
    }
}
