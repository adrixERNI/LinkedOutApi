using System;

namespace LinkedOutApi.DTOs.Certifications;

public class CertificationsAddDTO{
    public string Name { get; set; }
    public string IssuingOrg {get; set;}
    public DateOnly Expiration { get; set; }

    public Guid UserId { get; set; }
    // public bool IsDeleted { get; set; }
    public int SkillId {get; set;}
}


    public class CertificationUpdateDTO{
        public string Name { get; set; }
        public string IssuingOrg {get; set;}
        public DateOnly Expiration { get; set; }
   
        public int SkillId {get; set;}
    }

    public class CertificationResponseDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string IssuingOrg { get; set; }
    public DateOnly Expiration { get; set; }

    public int SkillId { get; set; }
}