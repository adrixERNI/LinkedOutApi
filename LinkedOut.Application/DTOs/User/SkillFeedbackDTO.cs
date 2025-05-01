namespace LinkedOutApi.DTOs.User
{
    public class SkillFeedbackDTO
    {
        public int SkillId { get; set; }
        public int? Rating { get; set; }
        public bool IsDeleted { get; set; }

    }

    public class PostSkillFeedbackDTO
    {
        public int TopicAssessmentId { get; set; }
        public List<SkillFeedbackDTO> SkillFeedback { get; set; }
        public Guid UserId { get; set; }

    }

    public class GetSkillFeedbackDTO
    {
        public int TopicAssessmentId { get; set; }
        public int SkillId { get; set; }
        public string SkillName { get; set; }
        public int? Rating { get; set; }
        public bool IsDeleted { get; set; }
        public Guid UserId { get; set; }

        //public string CreatedByName { get; set; }
    }
}
