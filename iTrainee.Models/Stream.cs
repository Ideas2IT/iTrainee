using System.ComponentModel.DataAnnotations;

namespace iTrainee.Models
{
    public class Stream : Base
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter stream name")]
        public string Name { get; set; }
    }
}
