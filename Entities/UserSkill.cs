namespace LinkedOutApi.Entities
{
    public class UserSkill
    {
        // bootcamper_self_rating
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid SkillId {  get; set; }
        public int Rating { get; set; }

        public User User { get; set; }
        public Skill Skill { get; set; }

    }
}
