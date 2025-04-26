using System;
using AutoMapper;
using LinkedOutApi.DTOs.User.TraineeFolder;
using LinkedOutApi.DTOs.User.TraineeFolder.BatchFolder;
using LinkedOutApi.DTOs.User.TraineeFolder.CVFolder;
using LinkedOutApi.DTOs.User.TraineeFolder.ImageFolder;
using LinkedOutApi.Entities;

namespace LinkedOutApi.Profiles.UserTraineeProfile;

public class UserTraineeProfile : Profile
{

   public UserTraineeProfile()
    {
    
        CreateMap<User, UserTraineeDTO>()
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => new ImageDTO
            {
                Name = src.Image.Name,
                Path = src.Image.Path 
            }))
            .ForMember(dest => dest.Resume, opt => opt.MapFrom(src => new CvDTO
            {
                Name = src.CV.Name,
                File = src.CV.File  
            }))
          .ForMember(dest => dest.Batch, opt => opt.MapFrom(src => new BatchDTO
            {
                Name = src.Batch.Name,
                Status = src.Batch.Status
            }))
            .ReverseMap();  
    }

}