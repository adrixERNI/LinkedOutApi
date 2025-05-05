using System;
using LinkedOutApi.DTOs.Certifications;
using LinkedOutApi.DTOs.User.UserFolder.BatchFolder;
using LinkedOutApi.DTOs.User.UserFolder.CVFolder;
using LinkedOutApi.DTOs.User.UserFolder.ImageFolder;

namespace LinkedOutApi.DTOs.User.TraineeFolder;

public class UserTraineeDTO
{
    public string Name { get; set; }
    public string Email { get; set; }
    public BatchReadDTO Batch { get; set; }
    public string Bio { get; set; }

    public ICollection<CertificationResponseDTO> Certification {get; set;}
    //public ImageDTO Image {get; set;}
    //public CvDTO Resume {get; set;}

}

public class UserTraineeCertificationDTO{
    public Guid Id {get;set;}
    public string Name {get; set;}
    public ICollection<CertificationResponseDTO> Certification {get; set;}
}

public class UserTraineeGetIdCertificationDTO{
    public Guid Id {get;set;}
    public string Name {get; set;}
    public CertificationResponseDTO Certification {get; set;}
}








