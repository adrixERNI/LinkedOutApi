namespace LinkedOutApi.DTOs.Projects
{
    public class CreateProjects
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string TechUsed { get; set; }
        public Guid UserId { get; set; }
    }
}