using AutoMapper;
using LinkedOutApi.DTOs.Shared;
using LinkedOutApi.Entities;

namespace LinkedOutApi.Profiles
{
    public class TopicProfiles : Profile
    {
        public TopicProfiles()
        {
            CreateMap<Topic, TopicReadDTO>()
                .ForMember(dest => dest.MentorName, opt => opt.MapFrom(src => src.User.Name))
                .ForMember(dest => dest.BatchName, opt => opt.MapFrom(src => src.Batch.Name));

            CreateMap<TopicAddDTO, Topic>();
            CreateMap<Topic, TopicReadDTO>()
                .ForMember(dest => dest.MentorName, opt => opt.MapFrom(src => src.User.Name))
                .ForMember(dest => dest.BatchName, opt => opt.MapFrom(src => src.Batch.Name))
                .ForMember(dest => dest.TopicSkill, opt => opt.MapFrom(src => src.TopicSkill));

            CreateMap<TopicAddRequestDTO, Topic>();

            CreateMap<TopicSkill, TopicSkillReadDTO>()
                .ForMember(dest => dest.SkillName, opt => opt.MapFrom(src => src.Skill.Name))
                .ReverseMap();
        }
    }
}
