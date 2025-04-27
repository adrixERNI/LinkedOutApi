using LinkedOutApi.DTOs.Shared;

namespace LinkedOutApi.Interfaces.Common
{
    public interface ITopicService
    {
        Task<TopicReadDTO> CreateTopicWithSkillAsync(TopicAddDTO topicAddDTO, List<int> skillIds);
        Task<TopicReadDTO> UpdateTopicAsync(int id, TopicAddDTO topicAddDTO, List<int> skillIds);
        Task<TopicReadDTO> DeleteTopicAsync(int id);
        Task<TopicReadDTO> GetTopicByIdAsync(int id);
        Task<ICollection<TopicReadDTO>> GetTopicsAsync();
    }
}
