namespace LinkedOutApi.DTOs.User
{
    public class MentorSkillFeedbackDTO
    {
        public int SkillId { get; set; }
        public int? Rating { get; set; }
        public bool IsDeleted { get; set; }

    }

    public class PostSkillFeedbackDTO
    {
        public int MentorAssessmentId { get; set; }
        public List<MentorSkillFeedbackDTO> SkillFeedback { get; set; }
    }

    public class GetSkillFeedbackDTO
    {
        public int MentorAssessmentId { get; set; }
        public int SkillId { get; set; }
        public string SkillName { get; set; }
        public int? Rating { get; set; }
        public bool IsDeleted { get; set; }
    }
}
