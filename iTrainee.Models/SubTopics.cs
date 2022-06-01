using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iTrainee.Models
{
    public class SubTopics : Base
    {
        public int Id { get; set; }

        public int StreamId { get; set; }

        public int TopicId { get; set; }

        [Required(ErrorMessage = "Please enter subtopic name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter reference URL")]
        [DataType(DataType.Url)]
        public string ReferenceURL { get; set; }

        [NotMapped]
        public virtual Topics Topic { get; set; }

        public string ReferenceURL { get; set; }

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
