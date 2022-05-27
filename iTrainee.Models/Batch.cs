using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iTrainee.Models
{
    public class Batch : Base
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter batch name")]
        public string Name { get; set; }

        [NotMapped]
        public IEnumerable<User> MentorList { get; set; }

        [NotMapped]
        public List<User> TraineeList { get; set; }

        [NotMapped]
        public IEnumerable<Stream> StreamList { get; set; }

        [Required(ErrorMessage = "Please select minimum one mentor")]
        public string[] SelectedMentorIds { get; set; }

        [Required(ErrorMessage = "Please select minimum one trainee")]
        public string[] SelectedTraineeIds { get; set; }

        [Required(ErrorMessage = "Please select minimum one stream")]
        public string[] SelectedStreamIds { get; set; }

        [NotMapped]
        public string StringUserIds { get; set; }

        [NotMapped]
        public string StringStreamIds { get; set; }
    }
}
