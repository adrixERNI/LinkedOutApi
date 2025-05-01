using LinkedOutApi.Entities;

namespace LinkedOutApi.Interfaces.User
{
    public interface ISkillFeedbackRepository
    {
        Task<List<SkillFeedback>> AddSkillFeedback(List<SkillFeedback> skillFeedback);
        Task<SkillFeedback?> UpdateSkillFeedback(int id, SkillFeedback skillFeedback);
        Task<SkillFeedback> DeleteSkillFeedback(int id);
        Task<IEnumerable<SkillFeedback>> GetAllSkillFeedbacks(int id);
    }
}
