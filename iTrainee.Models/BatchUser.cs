namespace iTrainee.Models
{
    public class BatchUser : Base
    {
        public int Id { get; set; }

        public int BatchId { get; set; }

        public int UserId { get; set; }

        public int StreamId { get; set; }
    }
}