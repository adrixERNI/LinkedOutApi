using LinkedOutApi.Data;
using LinkedOutApi.Entities;
using LinkedOutApi.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LinkedOutApi.Repositories 
{
    public class CVRepository : ICVRepository
    {
        private readonly AppDbContext _context;

        public CVRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CV>> GetAllCVsAsync()
        {
            return await _context.CVs.ToListAsync();
        }

        public async Task<CV> GetCVByIdAsync(int id)
        {
            return await _context.CVs.FindAsync(id);
        }

        public async Task AddCVAsync(CV cv)
        {
            await _context.CVs.AddAsync(cv);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCVAsync(CV cv)
        {
            _context.CVs.Update(cv);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCVAsync(int id)
        {
            var cv = await GetCVByIdAsync(id);
            if (cv != null)
            {
                _context.CVs.Remove(cv);
                await UpdateCVAsync(cv);
            }
        }
    }


}