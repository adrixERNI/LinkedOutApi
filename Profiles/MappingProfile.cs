using AutoMapper;
using LinkedOutApi.DTOs.User;
using LinkedOutApi.Entities;

namespace LinkedOutApi.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateAssessmentDTO, TopicAssessment>().ReverseMap();


            CreateMap<PostUserSkillDTO, UserSkill>().ReverseMap();

            CreateMap<UserSkillDTO, UserSkill>()
                .ForMember(dest => dest.UserId, opt => opt.Ignore());

            CreateMap<UserSkill, UserSkillDTO>();

            CreateMap<UserSkill, GetUserSkillDTO>()
                .ForMember(dest => dest.SkillName, opt => opt.MapFrom(src => src.Skill.Name));


            CreateMap<PostSkillFeedbackDTO, SkillFeedback>().ReverseMap();

            CreateMap<SkillFeedbackDTO, SkillFeedback>()
                .ForMember(dest => dest.TopicAssessmentId, opt => opt.Ignore());

            CreateMap<SkillFeedback, SkillFeedbackDTO>().ReverseMap();

            CreateMap<SkillFeedback, GetSkillFeedbackDTO>()
                .ForMember(dest => dest.SkillName, opt => opt.MapFrom(src => src.Skill.Name));
                //.ForMember(dest => dest.CreatedByRole, opt => opt.MapFrom(src => src.User.Role.Name));

        }
    }
}
