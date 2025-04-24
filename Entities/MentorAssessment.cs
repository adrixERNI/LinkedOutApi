namespace LinkedOutApi.Entities
{
    public class MentorAssessment
    {
        // bootcamp_trainee_assessment
        public Guid Id { get; set; }
        public int OverallRating { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Tags { get; set; }

        public Guid MentorId { get; set; }
        public Guid BootcamperId { get; set; }
        public Guid TopicId { get; set; }

        public User User { get; set; }
        public Topic Topic { get; set; }
    }
}
