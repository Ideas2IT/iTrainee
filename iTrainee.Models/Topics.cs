using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iTrainee.Models
{
    public class Topics : Base
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "Please select stream")]
        public int StreamId { get; set; }

        [Required(ErrorMessage = "Please enter topic name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter reference URL")]
        [DataType(DataType.Url)]
        public string ReferenceURL { get; set; }

        [NotMapped]
        public string StreamName { get; set; }

        [NotMapped]
        public virtual List<SubTopics> SubTopics { get; set; }

    }
}
