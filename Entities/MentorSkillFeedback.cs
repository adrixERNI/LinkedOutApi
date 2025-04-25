namespace LinkedOutApi.Entities
{
    public class MentorSkillFeedback
    {
        public int Id { get; set; }
        public int Rating { get; set; }

        public int MentorAssessmentId { get; set; }
        public int SkillId { get; set; }

        public MentorAssessment MentorAssessment { get; set; }
        public Skill Skill { get; set; }
    }
}
