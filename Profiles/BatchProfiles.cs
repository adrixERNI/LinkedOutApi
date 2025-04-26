using AutoMapper;
using LinkedOutApi.DTOs.User.UserFolder.BatchFolder;
using LinkedOutApi.Entities;

namespace LinkedOutApi.Profiles
{
    public class BatchProfiles : Profile
    {
        public BatchProfiles() 
        {
            CreateMap<Batch, BatchReadDTO>().ReverseMap();
            CreateMap<Batch, BatchUpdateDTO>().ReverseMap();
            CreateMap<Batch, BatchCreateDTO>().ReverseMap();
        }
    }
}
