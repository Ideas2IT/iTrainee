using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace iTrainee.Models
{
    public class UserAudit : Base
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public DateTime Date { get; set; }

        public DateTime SignIn { get; set; }

        public DateTime SignOut { get; set; }

        [NotMapped]
        public string[] AssignedTopicsArray { get; set; }

        [NotMapped]
        public string[] AssignedSubTopicsArray { get; set; }

        [NotMapped]
        public string[] CompletedSubTopicsArray { get; set; }

        [NotMapped]
        public IEnumerable<Topics> AssignedTopicsList { get; set; }

        [NotMapped]
        public IEnumerable<SubTopics> AssignedSubTopicsList { get; set; }
    }
}
