namespace LinkedOutApi.Entities
{
    public class SkillFeedback
    {
        public int Id { get; set; }
        public int? Rating { get; set; }

        public int TopicAssessmentId { get; set; }
        public int SkillId { get; set; }
        public bool IsDeleted { get; set; }
        public Guid UserId { get; set; }

        public TopicAssessment TopicAssessment { get; set; }
        public User User { get; set; }
        public Skill Skill { get; set; }
    }
}
