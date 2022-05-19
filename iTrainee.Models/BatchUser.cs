using System.Collections.Generic;

namespace iTrainee.Models
{
    public class BatchUser : Base
    {
        public int Id { get; set; }

        public int BatchId { get; set; }

        public int UserId { get; set; }

        public int StreamId { get; set; }
        public List<User> TopicsList { get; set; }
        public List<User> SubTopicsList { get; set; }
    }
}