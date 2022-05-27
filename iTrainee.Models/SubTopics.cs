using System.ComponentModel.DataAnnotations.Schema;

namespace iTrainee.Models
{
    public class SubTopics : Base
    {
        public int Id { get; set; }

        public int TopicId { get; set; }

        public string Name { get; set; }

        [NotMapped]
        public string TopicName { get; set; }

        [NotMapped]
        public virtual Topics Topic { get; set; }

        public string ReferenceURL { get; set; }
    }
}
