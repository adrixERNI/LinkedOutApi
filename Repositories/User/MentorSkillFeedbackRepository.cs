using Microsoft.EntityFrameworkCore;
using LinkedOutApi.Data;
using LinkedOutApi.Entities;
using LinkedOutApi.Interfaces.User;

namespace LinkedOutApi.Repositories.User
{
    public class MentorSkillFeedbackRepository : IMentorSkillFeedbackRepository
    {
        private readonly AppDbContext _context;

        public MentorSkillFeedbackRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<MentorSkillFeedback>> AddSkillFeedback(List<MentorSkillFeedback> mentorSkillFeedback)
        {
            await _context.AddRangeAsync(mentorSkillFeedback);
            await _context.SaveChangesAsync();
            return mentorSkillFeedback;
        }

        public async Task<MentorSkillFeedback> DeleteSkillFeedback(int id)
        {
            var skillFeedback = await _context.MentorSkillFeedbacks.FindAsync(id);
            if (skillFeedback == null)
            {
                throw new KeyNotFoundException("Id does not exist.");
            }
            skillFeedback.IsDeleted = true;
            await _context.SaveChangesAsync();
            return skillFeedback;
        }

        public async Task<IEnumerable<MentorSkillFeedback>> GetAllSkillFeedbacks(int id)
        {
            var skillFeedbackList = await _context.MentorSkillFeedbacks
                .Where(u => u.IsDeleted == false && u.MentorAssessmentId == id)
                .Include(u => u.Skill)
                .ToListAsync();
            return skillFeedbackList;
        }

        public async Task<MentorSkillFeedback?> UpdateSkillFeedback(int id, MentorSkillFeedback mentorSkillFeedback)
        {
            var currentSkillFeedback = await _context.MentorSkillFeedbacks.FindAsync(id);

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
