using LinkedOutApi.Entities;

namespace LinkedOutApi.DTOs.Shared
{
    public class TopicReadDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string BatchName { get; set; }
        public string MentorName { get; set; } // mentor
        public bool IsDeleted { get; set; }

        public int BatchId { get; set; }

        public TopicSkillReadDTO TopicSkill { get; set; }
    }

    public class TopicAddDTO
    {
        public string Name { get; set; }
        public int BatchId { get; set; }
        public Guid UserId { get; set; }
    }

    public class TopicAddRequestDTO
    {
        public string Name { get; set; }
        public Guid UserId { get; set; }
        public int BatchId { get; set; }
        List<int> SkillIds { get; set; }
    }
}
