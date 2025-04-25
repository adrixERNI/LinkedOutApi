namespace LinkedOutApi.Entities
{
    public class Certification
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateOnly Expiration { get; set; }
        public Guid UserId { get; set; }
        public bool IsDeleted { get; set; }

        public User User { get; set; }
    }
}
