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
        public int TopicId { get; set; }

        [NotMapped]
        public string SubTopicName { get; set; }

        public string id { get; set; }

        public string parent { get; set; }

        public string text { get; set; }

        [NotMapped]
        public string SelectedTraineeList { get; set; }

        [NotMapped]
        public string SelectedSubTopicList { get; set; }


        [NotMapped]
        public string[] SelectedTrainees { get; set; }

        [NotMapped]
        public string[] SelectedSubtopics { get; set; }

        [NotMapped]
        public List<User> TraineeList { get; set; }

        [NotMapped]
        public virtual List<Topics> TopicList { get; set; }
    }
}
