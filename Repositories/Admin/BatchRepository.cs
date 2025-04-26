using LinkedOutApi.Data;
using LinkedOutApi.Entities;
using LinkedOutApi.Interfaces.Admin;
using Microsoft.EntityFrameworkCore;

namespace LinkedOutApi.Repositories.Admin
{
    public class BatchRepository : IBatchRepository
    {
        private readonly AppDbContext _context;

        public BatchRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Batch> CreateBatchAsync(Batch batch)
        {
            var createdBatch = await _context.AddAsync(batch);
            await _context.SaveChangesAsync();
            return batch;
        }

        public async Task<Batch> DeleteBatchAsync(int id)
        {
            var batchToDelete = await _context.Batches.FirstOrDefaultAsync(b => b.Id == id && b.IsDeleted == false);

            if (batchToDelete == null)
            {
                throw new KeyNotFoundException("Batch doesn't exist.");
            }
            batchToDelete.IsDeleted = true;
            await _context.SaveChangesAsync();
            return batchToDelete;
        }

        public async Task<Batch> GetBatchByIdAsync(int id)
        {
            var batch = await _context.Batches.FirstOrDefaultAsync(b => b.Id == id && b.IsDeleted == false);
            if (batch == null)
            {
                throw new KeyNotFoundException("Batch with that Id doesn't exist");
            }
            return batch;
        }

        public async Task<ICollection<Batch>> GetBatchesAsync()
        {
            var batches = await _context.Batches.Where(b => b.IsDeleted == false).ToListAsync();
            return batches;
        }

        public async Task<Batch> UpdateBatchAsync(int id, Batch batch)
        {
            var batchToEdit = await _context.Batches.FindAsync(id);
            if (batchToEdit == null)
            {
                throw new KeyNotFoundException("No Batch Found");
            }

            batchToEdit.Name = batch.Name;
            batchToEdit.Status = batch.Status;
            await _context.SaveChangesAsync();
            return batchToEdit;
        }
    }
}
