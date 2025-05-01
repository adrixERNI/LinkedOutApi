using LinkedOutApi.Entities;

namespace LinkedOutApi.Entities
{
    public class CV
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string File { get; set; }
        public bool IsDeleted { get; set; }
        public Guid UserId { get; set; }
        public User? User { get; set; }
    }
}
