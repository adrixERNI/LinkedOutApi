namespace LinkedOutApi.DTOs
{
    public class CreateAssessmentDTO
    {
        public Guid MentorId { get; set; }
        public Guid BootcamperId { get; set; }
        public int TopicId { get; set; }
        public int OverallRating { get; set; }
        public string? Comments { get; set; }
        public string? Tags { get; set; }
    }
}
