using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace iTrainee.Models
{
    public class UserTopics : Base
    {
       public int Id { get; set; }
        public string Name { get; set; }
        public string TopicName { get; set; }
        public string SubTopicName { get; set; }
        public string id { get; set; }
        public string parent { get; set; }
        public string text { get; set; }

        public int TopicId { get; set; }

        [NotMapped]
        public List<Topics> TopicsList { get; set; }

        [NotMapped]
        public List<User> TraineeList { get; set; }
       

        public string SelectedTraineeList { get; set; }

        [NotMapped]
        public string SelectedSubTopicList { get; set; }

        [NotMapped]
        public string[] SelectedTrainees { get; set; }
    }
}
