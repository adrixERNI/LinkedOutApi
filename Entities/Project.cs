namespace LinkedOutApi.Entities
{
    public class Project
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string TechUsed { get; set; }
        public string RepoLink { get; set; }
        public Guid UserId { get; set; }

        public User User { get; set; }
    }
}
