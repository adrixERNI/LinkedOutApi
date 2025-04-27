using System;
using AutoMapper;
using LinkedOutApi.DTOs.Skills;
using LinkedOutApi.Entities;

namespace LinkedOutApi.Profiles;

public class SkillProfile:Profile
{
    public SkillProfile(){
        CreateMap<Skill,SkillDTO>().ReverseMap();
    }
}
