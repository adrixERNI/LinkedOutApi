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
                .Include(u => u.Image)
                .Include(u => u.CV)
                .Include(u => u.Role)
                .Where(u=> u.RoleId == 1)
                .ToListAsync();
    }

    public async Task<List<Entities.User>> GetAllMentorAsync(){
        return await _context.Users
                .Include(u => u.Batch)
                .Include(u => u.Image)
                .Include(u => u.Role)
                .Where(u=> u.RoleId == 2)
                .ToListAsync();
                 
    }
    
}
