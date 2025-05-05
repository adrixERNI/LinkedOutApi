using System;
using Microsoft.EntityFrameworkCore;
using LinkedOutApi.Data;
using LinkedOutApi.Entities;

namespace LinkedOutApi.Repositories.UserRepostiory;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Entities.User>> GetAllUserAsync(){
        return await _context.Users
                .Include(u => u.Batch)
                //.Include(u => u.Image)
                //.Include(u => u.CV)
                .Include(u => u.Role)
                .Where(u=> u.RoleId == 1)
                .Include(u => u.Certifications)
                .ToListAsync();
    }

    public async Task<List<Entities.User>> GetAllMentorAsync(){
        return await _context.Users
                .Include(u => u.Batch)
                //.Include(u => u.Image)
                .Include(u => u.Role)
                .Where(u=> u.RoleId == 2)
                .ToListAsync();
                 
    }

    public async Task<bool> AddUsersToBatch(int batchId, List<Guid> userIds)
    {
        var users = await  _context.Users
            .Where(u => userIds.Contains(u.Id))
            .ToListAsync();

        if (users.Count != userIds.Count)
        {
            return false; // Some users not found
        }

        foreach (var user in users)
        {
            user.BatchId = batchId;
        }

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> RemoveUsersFromBatch(int batchId, List<Guid> userIds)
    {
        var users = await _context.Users
            .Where(u => userIds.Contains(u.Id))
            .ToListAsync();

        if (users.Count != userIds.Count)
        {
            return false; // Some users not found
        }

        foreach (var user in users)
        {
            user.BatchId = null;
        }

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<Entities.User> GetByIdTraineeCertificationAsync(Guid id)
    {
       return await _context.Users
        .Include(c => c.Certifications)
        .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Entities.User> GetByIdTraineeAndCertificationAsync(Guid userId, int certificationId)
 {
    var user = await _context.Users
        .Include(u => u.Certifications)
        .FirstOrDefaultAsync(u => u.Id == userId && u.Certifications.Any(c => c.Id == certificationId));

    if (user == null)
        return null;

    user.Certifications = user.Certifications
        .Where(c => c.Id == certificationId)
        .ToList();

    return user;
    }
}
