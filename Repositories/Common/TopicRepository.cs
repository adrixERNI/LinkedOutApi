using LinkedOutApi.Data;
using LinkedOutApi.Entities;
using LinkedOutApi.Interfaces.Common;
using Microsoft.EntityFrameworkCore;

namespace LinkedOutApi.Repositories.Common
{
    public class TopicRepository : ITopicRepository
    {
        private readonly AppDbContext _context;

        public TopicRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Topic> CreateTopicWithSkillAsync(Topic topic, List<int> skillIds)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                _context.Topics.Add(topic);
                await _context.SaveChangesAsync();

                var topicSkills = skillIds.Select(skillId => new TopicSkill
                {
                    TopicId = topic.Id,
                    SkillId = skillId,
                    IsDeleted = false
                }).ToList();

                _context.TopicSkills.AddRange(topicSkills);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return topic;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<Topic> DeleteTopicAsync(int id)
        {
            var topicToDelete = await _context.Topics.FindAsync(id);
            if(topicToDelete == null)
            {
                throw new KeyNotFoundException("No topic with that key.");
            }
            topicToDelete.IsDeleted = true;
            await _context.SaveChangesAsync();
            return topicToDelete;
        }

        public async Task<Topic> GetTopicByIdAsync(int id)
        {
            var topic = await _context.Topics
                .Include(t => t.TopicSkill)
                .FirstOrDefaultAsync(t => t.Id == id && t.IsDeleted == false);

            if (topic == null)
            {
                throw new KeyNotFoundException("No topic with that key.");
            }

            return topic;
        }

        public async Task<ICollection<Topic>> GetTopicsAsync()
        {
            var topics = await _context.Topics
                .Include(t => t.TopicSkill)
                .Where(t => t.IsDeleted == false).ToListAsync();
            return topics;
        }

        public async Task<Topic> UpdateTopicAndSkillAsync(int id, Topic topic, List<int> skillIds)
        {
            var existingTopic = await _context.Topics
                .Include(t => t.TopicSkill)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (existingTopic == null)
                throw new KeyNotFoundException("Topic not found");

            existingTopic.Name = topic.Name;
            existingTopic.BatchId = topic.BatchId;
            existingTopic.UserId = topic.UserId;
            existingTopic.IsDeleted = topic.IsDeleted;

            var skillsToDelete = existingTopic.TopicSkill
                .Where(ts => !skillIds.Contains(ts.SkillId))
            .ToList();

            _context.TopicSkills.RemoveRange(skillsToDelete);

            var currentSkillIds = existingTopic.TopicSkill.Select(ts => ts.SkillId).ToList();
            var newSkillIds = skillIds.Except(currentSkillIds).ToList(); 

            var newTopicSkills = newSkillIds.Select(skillId => new TopicSkill
            {
                TopicId = existingTopic.Id,
                SkillId = skillId,
                IsDeleted = false
            }).ToList();

            _context.TopicSkills.AddRange(newTopicSkills);

            await _context.SaveChangesAsync();

            return existingTopic;
        }
    }
}
