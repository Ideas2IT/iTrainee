using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace iTrainee.Models
{
    public class UserTopics : Base
    {
        [NotMapped]
        public string Name { get; set; }

        [NotMapped]
        public string TopicName { get; set; }

        [NotMapped]
        public string SubTopicName { get; set; }

        [NotMapped]
        public string id { get; set; }

        [NotMapped]
        public string parent { get; set; }

        [NotMapped]
        public string text { get; set; }

        [NotMapped]
        public int TopicId { get; set; }

        public string SelectedTraineeList { get; set; }

        [NotMapped]
        public string SelectedSubTopicList { get; set; }

        [NotMapped]
        public string[] SelectedTrainees { get; set; }

        [NotMapped]
        public List<User> TraineeList { get; set; }
    }
}
