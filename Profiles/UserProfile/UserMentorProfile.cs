using System;
using AutoMapper;
using LinkedOutApi.DTOs.User.TraineeFolder;
using LinkedOutApi.DTOs.User.UserFolder;
using LinkedOutApi.DTOs.User.UserFolder.BatchFolder;
using LinkedOutApi.DTOs.User.UserFolder.ImageFolder;
using LinkedOutApi.Entities;

namespace LinkedOutApi.Profiles.UserProfile;

public class UserMentorProfile : Profile
{
    public UserMentorProfile(){
        CreateMap<User, UserMentorDTO>()
            .ForMember(dest => dest.Batch, opt => opt.MapFrom(src => new BatchDTO
            {
                Name = src.Batch.Name,
                Status = src.Batch.Status
            }))
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => new ImageDTO
            {
                Name = src.Image.Name,
                Path = src.Image.Path
            }))
            .ReverseMap();
            
    }
}
