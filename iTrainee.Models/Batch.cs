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

        public IEnumerable<User> MentorList { get; set; }

        public List<User> TraineeList { get; set; }

        public IEnumerable<Stream> StreamList { get; set; }

        [Required(ErrorMessage = "Please select minimum one mentor")]
        public string[] SelectedMentorIds { get; set; }

        [Required, MinLength(1, ErrorMessage = "At least one item required")]
        public string[] SelectedTraineeIds { get; set; }

        [Required, MinLength(1, ErrorMessage = "At least one item required")]
        public string[] SelectedStreamIds { get; set; }

        [NotMapped]
        public string StringUserIds { get; set; }

        [NotMapped]
        public string StringStreamIds { get; set; }
    }
}
