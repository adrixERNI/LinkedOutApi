namespace LinkedOutApi.Entities
{
    public class Image
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public bool IsDeleted { get; set; }
        public Guid UserId { get; set; }

        public User User { get; set; }
    }
}
