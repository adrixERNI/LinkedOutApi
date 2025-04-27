using System;
using LinkedOutApi.Entities;

namespace LinkedOutApi.Interfaces.Common;

public interface ISkillRepository
{
 Task<List<Skill>> GetAllSkillAsync();
}
