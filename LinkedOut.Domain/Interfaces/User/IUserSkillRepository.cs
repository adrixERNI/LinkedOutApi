using LinkedOutApi.Entities;

namespace LinkedOutApi.Interfaces.User
{
    public interface IUserSkillRepository
    {
        public Task<IEnumerable<UserSkill>> GetAllUserSkills(Guid id);
        public Task<List<UserSkill>> AddUserSkill(List<UserSkill> userSkills);
        public Task<UserSkill?> UpdateUserSkill(int id, UserSkill userSkill);
        public Task<UserSkill> DeleteUserSkill(int id);
    }
}
