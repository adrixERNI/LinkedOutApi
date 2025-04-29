using AutoMapper;
using LinkedOutApi.DTOs.User;
using LinkedOutApi.Entities;

namespace LinkedOutApi.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateAssessmentDTO, MentorAssessment>().ReverseMap();


            CreateMap<PostUserSkillDTO, UserSkill>().ReverseMap();

            CreateMap<UserSkillDTO, UserSkill>()
                .ForMember(dest => dest.UserId, opt => opt.Ignore());

            CreateMap<UserSkill, UserSkillDTO>();

            CreateMap<UserSkill, GetUserSkillDTO>()
                .ForMember(dest => dest.SkillName, opt => opt.MapFrom(src => src.Skill.Name));


            CreateMap<PostSkillFeedbackDTO, MentorSkillFeedback>().ReverseMap();

            CreateMap<MentorSkillFeedbackDTO, MentorSkillFeedback>()
                .ForMember(dest => dest.MentorAssessmentId, opt => opt.Ignore());

            CreateMap<MentorSkillFeedback, MentorSkillFeedbackDTO>().ReverseMap();

            CreateMap<MentorSkillFeedback, GetSkillFeedbackDTO>()
                .ForMember(dest => dest.SkillName, opt => opt.MapFrom(src => src.Skill.Name));

        }
    }
}
