namespace iTrainee.Models
{
    public class Messages : Base
    {
        public int Id { get; set; }

        public int FromId { get; set; }

        public string Message { get; set; }
    }
}
