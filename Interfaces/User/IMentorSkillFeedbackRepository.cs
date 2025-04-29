using LinkedOutApi.Entities;

namespace LinkedOutApi.Interfaces.User
{
    public interface IMentorSkillFeedbackRepository
    {
        Task<List<MentorSkillFeedback>> AddSkillFeedback(List<MentorSkillFeedback> mentorSkillFeedback);
        Task<MentorSkillFeedback?> UpdateSkillFeedback(int id, MentorSkillFeedback mentorSkillFeedback);
        Task<MentorSkillFeedback> DeleteSkillFeedback(int id);
        Task<IEnumerable<MentorSkillFeedback>> GetAllSkillFeedbacks(int id);
    }
}
