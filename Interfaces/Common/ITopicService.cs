using LinkedOutApi.DTOs.Shared;

namespace LinkedOutApi.Interfaces.Common
{
    public interface ITopicService
    {
        Task<TopicReadDTO> CreateTopicWithSkillAsync(TopicAddRequestDTO topicAddDTO);
        Task<TopicReadDTO> UpdateTopicAsync(int id, TopicAddRequestDTO topicAddDTO);
        Task<TopicReadDTO> DeleteTopicAsync(int id);
        Task<TopicReadDTO> GetTopicByIdAsync(int id);
        Task<ICollection<TopicReadDTO>> GetTopicsAsync();
    }
}
