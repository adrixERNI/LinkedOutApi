using Microsoft.EntityFrameworkCore;
using AutoMapper;
using LinkedOutApi.Data;
using LinkedOutApi.Entities;
using LinkedOutApi.Interfaces.Mentor;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace LinkedOutApi.Repositories.Mentor
{
    public class MentorAssessmentRepository : IMentorAssessmentRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public MentorAssessmentRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<MentorAssessment> AddMentorAssessment(MentorAssessment mentorAssessment)
        {
            mentorAssessment.CreatedAt = DateTime.Now;
            mentorAssessment.IsDeleted = false;
            _context.Add(mentorAssessment);
            await _context.SaveChangesAsync();
            return mentorAssessment;    
        }

        public async Task<MentorAssessment> DeleteMentorAssessment(int id)
        {
            var assessment = await _context.MentorAssessments.FindAsync(id);
            if (assessment == null)
            {
                throw new ArgumentNullException("Assessment does not exist"); // throw exception here maybe?
            }
            assessment.IsDeleted = true;
            await _context.SaveChangesAsync();
            return assessment;
        }

        public async Task<IEnumerable<MentorAssessment>> GetAllMentorAssessments()
        {
            var assessmentList = await _context.MentorAssessments.Where(a => a.IsDeleted == false).ToListAsync();
            return assessmentList;
        }

        public async Task<MentorAssessment> GetMentorAssessmentById(int id)
        {
            var getAssessment = await _context.MentorAssessments.FirstOrDefaultAsync(a => a.Id == id);
            return getAssessment;
        }

        public async Task<MentorAssessment> UpdateMentorAssessment(int id, MentorAssessment mentorAssessment)
        {
            var currentAssessment = await _context.MentorAssessments.FirstOrDefaultAsync(a=> a.Id == id);
            currentAssessment.UpdatedAt = DateTime.Now;
            await _context.SaveChangesAsync();
            return currentAssessment;
        }
    }
}
