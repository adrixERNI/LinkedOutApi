namespace LinkedOutApi.Entities
{
    public class Certification
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string IssuingOrg {get; set;}
        public DateOnly Expiration { get; set; }

        public Guid UserId { get; set; }
        public bool IsDeleted { get; set; }

      
        public User User { get; set; }

        public Skill Skill {get; set;}
        public int SkillId {get; set;}

    }
}
