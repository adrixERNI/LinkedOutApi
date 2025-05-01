using Microsoft.EntityFrameworkCore;
using LinkedOutApi.Data;
using LinkedOutApi.Entities;
using LinkedOutApi.Interfaces.User;

namespace LinkedOutApi.Repositories.User
{
    public class SkillFeedbackRepository : ISkillFeedbackRepository
    {
        private readonly AppDbContext _context;

        public SkillFeedbackRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<SkillFeedback>> AddSkillFeedback(List<SkillFeedback> mentorSkillFeedback)
        {
            await _context.AddRangeAsync(mentorSkillFeedback);
            await _context.SaveChangesAsync();
            return mentorSkillFeedback;
        }

        public async Task<SkillFeedback> DeleteSkillFeedback(int id)
        {
            var skillFeedback = await _context.SkillFeedbacks.FindAsync(id);
            if (skillFeedback == null)
            {
                throw new KeyNotFoundException("Id does not exist.");
            }
            skillFeedback.IsDeleted = true;
            await _context.SaveChangesAsync();
            return skillFeedback;
        }

        public async Task<IEnumerable<SkillFeedback>> GetAllSkillFeedbacks(int id)
        {
            var skillFeedbackList = await _context.SkillFeedbacks
                .Where(u => u.IsDeleted == false && u.TopicAssessmentId == id)
                .Include(u => u.Skill)
                .ToListAsync();
            return skillFeedbackList;
        }

        public async Task<SkillFeedback?> UpdateSkillFeedback(int id, SkillFeedback mentorSkillFeedback)
        {
            var currentSkillFeedback = await _context.SkillFeedbacks.FindAsync(id);

            if (currentSkillFeedback == null)
            {
                throw new KeyNotFoundException("SkillFeedback Id not found.");
            }

            if (mentorSkillFeedback.Rating.HasValue)
            {
                currentSkillFeedback.Rating = mentorSkillFeedback.Rating;
            }

            await _context.SaveChangesAsync();
            return currentSkillFeedback;
        }
    }
}
