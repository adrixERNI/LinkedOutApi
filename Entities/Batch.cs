namespace LinkedOutApi.Entities
{
    public class Batch
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public bool IsDeleted { get; set; }

        public ICollection<User> Users { get; set; }
        public ICollection<Topic> Topics { get; set; }
    }
}
