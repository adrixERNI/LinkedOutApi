namespace LinkedOutApi.Entities
{
    public class TopicSkill
    {
        public Guid Id { get; set; }

        public Guid TopicId { get; set; }
        public Guid SkillId { get; set; }

        public Topic Topic { get; set; }
        public Skill Skill { get; set; }
    }
}
