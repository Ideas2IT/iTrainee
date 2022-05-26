using System.ComponentModel.DataAnnotations.Schema;

namespace iTrainee.Models
{
    public class SubTopics : Base
    {
        public int Id { get; set; }

        public int TopicId { get; set; }

        public string Name { get; set; }

        public string ReferenceURL { get; set; }

        [NotMapped]
        public string TopicName { get; set; }

        [NotMapped]
        public int Percentage { get; set; }

        [NotMapped]
        public int UserId { get; set; }

        [NotMapped]
        public string TraineeComments { get; set; }

        [NotMapped]
        public string MentorComments { get; set; }

        [NotMapped]
        public string Status { get; set; }
    }
}
