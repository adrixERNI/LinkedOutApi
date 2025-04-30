using System;
using LinkedOutApi.Entities;

namespace LinkedOutApi.Interfaces.Common;

public interface ISkillRepository
{
 Task<List<Skill>> GetAllSkillAsync();

 Task<bool> SkillExistingAsync(int skillId);

 Task<List<Skill>> GetAllSkillSelfAsync();

 Task<Skill> CreateSelfSkillAsync(Skill skill);

Task<Skill> DeleteSelfSkillAsync(int id);

      Task<Certification> UpdateCertificationAsync(int id, CertificationUpdateDTO cert);

}
