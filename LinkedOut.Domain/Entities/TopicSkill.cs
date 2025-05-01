namespace LinkedOutApi.Entities
{
    public class TopicSkill
    {
        public int Id { get; set; }

        public int TopicId { get; set; }
        public int SkillId { get; set; }
        public bool IsDeleted { get; set; }

        public Topic Topic { get; set; }
        public Skill Skill { get; set; }
    }
}
