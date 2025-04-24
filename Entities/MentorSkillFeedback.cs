namespace LinkedOutApi.Entities
{
    public class MentorSkillFeedback
    {
        public Guid Id { get; set; }
        public int Rating { get; set; }

        public Guid MentorAssessmentId { get; set; }
        public Guid SkillId { get; set; }

        public MentorAssessment MentorAssessment { get; set; }
        public Skill Skill { get; set; }
    }
}
