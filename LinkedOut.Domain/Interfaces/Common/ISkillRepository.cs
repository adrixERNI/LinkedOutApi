using System;
using LinkedOutApi.DTOs.Skills;
using LinkedOutApi.Entities;

namespace LinkedOutApi.Interfaces.Common;

public interface ISkillRepository
{
 Task<List<Skill>> GetAllSkillAsync();

 Task<bool> SkillExistingAsync(int skillId);

 Task<List<Skill>> GetAllSkillSelfAsync();

 Task<Skill> CreateSelfSkillAsync(Skill skill);

Task<Skill> DeleteSelfSkillAsync(int id);

Task<Skill> UpdateSelfSkillAsync(int id, SelfSkillUpdateDTO skill);


}
