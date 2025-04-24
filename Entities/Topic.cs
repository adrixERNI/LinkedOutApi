namespace LinkedOutApi.Entities
{
    public class Topic
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid BatchId { get; set; }
        public Guid UserId { get; set; } // mentor

        public Batch Batch { get; set; }
        public User User { get; set; }
    }
}
