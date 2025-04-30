namespace LinkedOutApi.DTOs.User
{
    public class TopicAssessmentDTO
    {
        public int Id { get; set; }
        public List<SkillFeedbackDTO> SkillFeedbackDTOs { get; set; }
    }
    public class CreateAssessmentDTO
    {
        public Guid MentorId { get; set; }
        public Guid BootcamperId { get; set; }
        public int TopicId { get; set; }
        public int OverallRating { get; set; }
        public string? Comments { get; set; }
        public string? Tags { get; set; }
    }

    public class GetAssessmentInfoDTO
    {
        public Guid MentorId { get; set; }
        public string MentorName { get; set; }
        public Guid BootcamperId { get; set; }
        public string BootcamperName { get; set; }
        public int TopicId { get; set; }
        public string TopicName { get; set; }
        public int OverallRating { get; set; }
        public string? Comments { get; set; }
        public string? Tags { get; set; }
    }

}
