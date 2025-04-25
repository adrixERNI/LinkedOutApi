using AutoMapper;
using LinkedOutApi.DTOs;
using LinkedOutApi.Entities;

namespace LinkedOutApi.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateAssessmentDTO, MentorAssessment>().ReverseMap();
        }
    }
}
