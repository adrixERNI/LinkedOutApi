using LinkedOutApi.Data;
using LinkedOutApi.Entities;
using LinkedOutApi.Interfaces.User;
using Microsoft.EntityFrameworkCore;

namespace LinkedOutApi.Repositories.Mentor
{
    public class TopicAssessmentRepository : ITopicAssessmentRepository
    {
        private readonly AppDbContext _context;

        public TopicAssessmentRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<TopicAssessment> AddTopicAssessment(TopicAssessment topicAssessment)
        {
            topicAssessment.CreatedAt = DateTime.Now;
            topicAssessment.IsDeleted = false;
            await _context.TopicAssessments.AddAsync(topicAssessment);
            await _context.SaveChangesAsync();

            if (topicAssessment.SkillFeedbacks != null && topicAssessment.SkillFeedbacks.Any())
            {
                foreach (var feedback in topicAssessment.SkillFeedbacks)
                {
                    feedback.TopicAssessmentId = topicAssessment.Id;
                    await _context.SkillFeedbacks.AddAsync(feedback);
                }
                await _context.SaveChangesAsync();
            }

            return topicAssessment;    
        }

        public async Task<TopicAssessment> DeleteTopicAssessment(int id)
        {
            var assessment = await _context.TopicAssessments.FindAsync(id);
            if (assessment == null)
            {
                throw new ArgumentNullException("Assessment does not exist"); // throw exception here maybe?
            }
            assessment.IsDeleted = true;
            await _context.SaveChangesAsync();
            return assessment;
        }

        public async Task<IEnumerable<TopicAssessment>> GetAllTopicAssessments()
        {
            var assessmentList = await _context.TopicAssessments.Where(a => a.IsDeleted == false).ToListAsync();
            return assessmentList;
        }

        public async Task<TopicAssessment> GetTopicAssessmentById(int id)
        {
            var getAssessment = await _context.TopicAssessments
                .Include(m => m.SkillFeedbacks)
                .FirstOrDefaultAsync(m => m.Id == id);

            return getAssessment;
        }

        public async Task<TopicAssessment> UpdateTopicAssessment(int id, TopicAssessment topicAssessment)
        {
            var currentAssessment = await _context.TopicAssessments.FirstOrDefaultAsync(a=> a.Id == id);
            currentAssessment.UpdatedAt = DateTime.Now;
            await _context.SaveChangesAsync();
            return currentAssessment;
        }
    }
}
