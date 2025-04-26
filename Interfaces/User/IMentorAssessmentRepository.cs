using LinkedOutApi.Entities;

namespace LinkedOutApi.Interfaces.User
{
    public interface IMentorAssessmentRepository
    {
        public Task<MentorAssessment> GetMentorAssessmentById(int id);
        public Task<MentorAssessment> AddMentorAssessment(MentorAssessment mentorAssessment);
        public Task<MentorAssessment> UpdateMentorAssessment(int id, MentorAssessment mentorAssessment);
        public Task<MentorAssessment> DeleteMentorAssessment(int id);
        public Task<IEnumerable<MentorAssessment>> GetAllMentorAssessments();
    }
}
