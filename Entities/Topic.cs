namespace LinkedOutApi.Entities
{
    public class Topic
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BatchId { get; set; }
        public Guid UserId { get; set; } // mentor
        public bool IsDeleted { get; set; }

        public Batch Batch { get; set; }
        public User User { get; set; }
    }
}
