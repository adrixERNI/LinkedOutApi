using System;
using LinkedOutApi.Entities;

namespace LinkedOutApi.Interfaces.Common;

public interface ISkillRepository
{
 Task<List<Skill>> GetAllSkillAsync();

 Task<bool> SkillExistingAsync(int skillId);
}
