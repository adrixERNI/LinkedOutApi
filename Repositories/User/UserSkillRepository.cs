using AutoMapper;
using LinkedOutApi.Data;
using LinkedOutApi.Entities;
using LinkedOutApi.Interfaces.User;
using Microsoft.EntityFrameworkCore;

namespace LinkedOutApi.Repositories.User
{
    public class UserSkillRepository : IUserSkillRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public UserSkillRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<UserSkill>> AddUserSkill(List<UserSkill> userSkills)
        {

            await _context.UserSkills.AddRangeAsync(userSkills);
            await _context.SaveChangesAsync();

            return userSkills;
        }

        public async Task<UserSkill> DeleteUserSkill(int id)
        {
            var selfRating = await _context.UserSkills.FindAsync(id);
            if (selfRating == null)
            {
                throw new KeyNotFoundException("Id does not exist"); // throw exception here maybe?
            }
            selfRating.IsDeleted = true;
            await _context.SaveChangesAsync();
            return selfRating;
        }

        public async Task<IEnumerable<UserSkill>> GetAllUserSkills(Guid id)
        {
            var selfRatingList = await _context.UserSkills
                .Where(u => u.IsDeleted == false)
                .Include(u => u.Skill)
                .ToListAsync();
            return selfRatingList;
        }

        public async Task<UserSkill?> UpdateUserSkill(int id, UserSkill userSkill)
        {
            var currentUserSkill = await _context.UserSkills.FindAsync(id);
            if (currentUserSkill == null)
            {
                throw new ArgumentNullException("UserSkill Id not found.");
            }

            if (userSkill.Rating.HasValue)
            {
                currentUserSkill.Rating = userSkill.Rating;
            }
            await _context.SaveChangesAsync();
            return currentUserSkill;
        }
    }
}
