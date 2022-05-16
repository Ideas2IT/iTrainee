namespace iTrainee.Models
{
    public class Topics : Base
    {
        public int Id { get; set; }

        public int StreamId { get; set; }

        public string Name { get; set; }

        public string ReferenceURL { get; set; }
        public string StreamName { get; set; }
    }
}
