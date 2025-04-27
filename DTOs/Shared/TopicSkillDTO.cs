using LinkedOutApi.Entities;

namespace LinkedOutApi.DTOs.Shared
{
    public class TopicSkillReadDTO
    {
        public int Id { get; set; }

        public int TopicId { get; set; }
        public int SkillId { get; set; }
        public bool IsDeleted { get; set; }
        public Skill Skill { get; set; }
    }
}
