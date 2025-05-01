using LinkedOutApi.Entities;

namespace LinkedOutApi.Interfaces.User
{
    public interface ITopicAssessmentRepository
    {
        public Task<TopicAssessment> GetTopicAssessmentById(int id);
        public Task<TopicAssessment> AddTopicAssessment(TopicAssessment topicAssessment);
        public Task<TopicAssessment> UpdateTopicAssessment(int id, TopicAssessment topicAssessment);
        public Task<TopicAssessment> DeleteTopicAssessment(int id);
        public Task<IEnumerable<TopicAssessment>> GetAllTopicAssessments();
    }
}
