using LinkedOutApi.Entities;

namespace LinkedOutApi.DTOs.User
{
    public class TopicReadDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string BatchName{ get; set; }
        public string MentorName{ get; set; } // mentor
        public bool IsDeleted { get; set; }

        public int BatchId { get; set; }
    }
}
