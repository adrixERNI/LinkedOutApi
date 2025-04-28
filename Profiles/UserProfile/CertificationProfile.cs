using System;
using AutoMapper;
using LinkedOutApi.DTOs.Certifications;
using LinkedOutApi.Entities;

namespace LinkedOutApi.Profiles.UserProfile;

public class CertificationProfile:Profile
{
    public CertificationProfile(){
        CreateMap<Certification,CertificationsAddDTO>().ReverseMap();
        CreateMap<Certification,CertificationUpdateDTO>().ReverseMap();

        CreateMap<Certification,CertificationResponseDTO>().ReverseMap();
    }
}
