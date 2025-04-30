namespace LinkedOutApi.Entities
{
    public class TopicAssessment
    {
        // bootcamp_trainee_assessment
        public int Id { get; set; }
        public Guid MentorId { get; set; }
        public Guid BootcamperId { get; set; }
        public int TopicId { get; set; }

        public int OverallRating { get; set; }
        public string? Comments { get; set; }
        public string? Tags { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }

        public User Mentor { get; set; }
        public User Bootcamper { get; set; }
        public Topic Topic { get; set; }

        public ICollection<SkillFeedback> SkillFeedbacks { get; set; }
    }
}
