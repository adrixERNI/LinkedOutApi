namespace LinkedOutApi.DTOs.User
{
    public class UserSkillDTO
    {
        public int SkillId { get; set; }
        public int? Rating { get; set; }
        public bool IsDeleted { get; set; }

    }
    public class PostUserSkillDTO
    {
        public Guid UserId { get; set; }
        public List<UserSkillDTO> SkillRatings { get; set; }
    }

    public class GetUserSkillDTO
    {
        public int SkillId { get; set; }
        public string SkillName { get; set; }
        public int? Rating { get; set; }
        public bool IsDeleted { get; set; }
    }

}
