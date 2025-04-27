using AutoMapper;
using LinkedOutApi.DTOs.Shared;
using LinkedOutApi.Entities;
using LinkedOutApi.Interfaces.Common;

namespace LinkedOutApi.Services.Shared
{
    public class TopicService : ITopicService
    {
        private readonly ITopicRepository _repository;
        private readonly IMapper _mapper;

        public TopicService(ITopicRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<TopicReadDTO> CreateTopicWithSkillAsync(TopicAddDTO topicAddDTO, List<int> skillIds)
        {
            var mappedTopic = _mapper.Map<Topic>(topicAddDTO);
            var topic = await _repository.CreateTopicWithSkillAsync(mappedTopic, skillIds);
            var mappedReadTopic = _mapper.Map<TopicReadDTO>(topic);
            return mappedReadTopic;
        }

        public async Task<TopicReadDTO> DeleteTopicAsync(int id)
        {
            var topic = await _repository.DeleteTopicAsync(id);
            return _mapper.Map<TopicReadDTO>(topic);
        }

        public async Task<TopicReadDTO> GetTopicByIdAsync(int id)
        {
            var topic = await _repository.GetTopicByIdAsync(id);
            var mappedTopic = _mapper.Map<TopicReadDTO>(topic);
            return mappedTopic;
        }

        public async Task<ICollection<TopicReadDTO>> GetTopicsAsync()
        {
            var topics = await _repository.GetTopicsAsync();
            var mappedTopic = _mapper.Map<ICollection<TopicReadDTO>>(topics);
            return mappedTopic;
        }

        public async Task<TopicReadDTO> UpdateTopicAsync(int id, TopicAddDTO topicAddDTO, List<int> skillIds)
        {
            var mappedTopic = _mapper.Map<Topic>(topicAddDTO);
            var updatedTopic = await _repository.UpdateTopicAndSkillAsync(id, mappedTopic, skillIds);
            var mappedUpdated = _mapper.Map<TopicReadDTO>(updatedTopic);
            return mappedUpdated;
        }
    }
}
