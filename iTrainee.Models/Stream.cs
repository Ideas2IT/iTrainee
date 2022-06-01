using System.ComponentModel.DataAnnotations;

namespace iTrainee.Models
{
    public class Stream : Base
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is Required")]
        public string Name { get; set; }
    }
}
