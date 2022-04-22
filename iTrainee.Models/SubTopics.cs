using System;
using System.Collections.Generic;
using System.Text;

namespace iTrainee.Models
{
    public class SubTopics : Base
    {
        public int Id { get; set; }

        public int TopicId { get; set; }

        public string Name { get; set; }

        public string ReferenceURL { get; set; }
    }
}
