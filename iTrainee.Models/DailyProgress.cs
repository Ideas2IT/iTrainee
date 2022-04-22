using System;
using System.Collections.Generic;
using System.Text;

namespace iTrainee.Models
{
    public class DailyProgress : Base
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int TopicId { get; set; }

        public int SubTopicId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Status { get; set; }

        public string TraineeComments { get; set; }

        public string MentorComments { get; set; }

        public float Percentage { get; set; }
    }
}
