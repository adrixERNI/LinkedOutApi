using System;
using AutoMapper;
using LinkedOutApi.DTOs.Certifications;
using LinkedOutApi.DTOs.User.TraineeFolder;
using LinkedOutApi.DTOs.User.UserFolder;
using LinkedOutApi.DTOs.User.UserFolder.BatchFolder;
using LinkedOutApi.DTOs.User.UserFolder.CVFolder;
using LinkedOutApi.DTOs.User.UserFolder.ImageFolder;
using LinkedOutApi.Entities;

namespace LinkedOutApi.Profiles.UserTraineeProfile;

public class UserTraineeProfile : Profile
{

   public UserTraineeProfile()
    {
    
        CreateMap<User, UserTraineeDTO>()
            //.ForMember(dest => dest.Image, opt => opt.MapFrom(src => new ImageDTO
            //{
            //    Name = src.Image.Name,
            //    Path = src.Image.Path 
            //}))
            //.ForMember(dest => dest.Resume, opt => opt.MapFrom(src => new CvDTO
            //{
            //    Name = src.CV.Name,
            //    File = src.CV.File  
            //}))
          .ForMember(dest => dest.Batch, opt => opt.MapFrom(src => new BatchReadDTO
            {
                Name = src.Batch.Name,
                Status = src.Batch.Status,
                IsDeleted = src.Batch.IsDeleted
            }))
            .ForMember(dest => dest.Certification, opt => opt.MapFrom(src => src.Certifications))
            .ReverseMap();

            CreateMap<User, UserTraineeCertificationDTO>()
                .ForMember(dest => dest.Certification, opt => opt.MapFrom(src => src.Certifications))
                .ReverseMap();
                
             CreateMap<User, UserTraineeGetIdCertificationDTO>()
            .ForMember(dest => dest.Certification, opt => opt.Ignore());
            
            
            
    }

}