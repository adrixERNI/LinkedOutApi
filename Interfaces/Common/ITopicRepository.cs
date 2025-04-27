using LinkedOutApi.Entities;

namespace LinkedOutApi.Interfaces.Common
{
    public interface ITopicRepository
    {
        Task<Topic> CreateTopicWithSkillAsync(Topic topic, List<int> skillIds);
        Task<Topic> UpdateTopicAndSkillAsync(int id, Topic topic, List<int> skillIds);
        Task<Topic> DeleteTopicAsync(int id);
        Task<Topic> GetTopicByIdAsync(int id);
        Task<ICollection<Topic>> GetTopicsAsync();
    }
}
