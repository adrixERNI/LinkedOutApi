using System;

namespace LinkedOutApi.DTOs.Certifications;

public class CertificationsAddDTO{
    public string Name { get; set; }
    public string IssuingOrg {get; set;}
    public DateOnly Expiration { get; set; }

    public string CredentialURL {get;set;}
    public Guid UserId { get; set; }
    // public bool IsDeleted { get; set; }
    public int SkillId {get; set;}
}
