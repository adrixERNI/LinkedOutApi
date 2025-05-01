namespace LinkedOutApi.Entities
{
    public class Project
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public string? TechUsed { get; set; }
        public Guid UserId { get; set; }
        public bool IsDeleted { get; set; }

        public User? User { get; set; }
    }
}
