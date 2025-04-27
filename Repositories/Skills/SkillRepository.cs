using System;
using LinkedOutApi.Data;
using LinkedOutApi.Entities;
using LinkedOutApi.Interfaces.Common;
using Microsoft.EntityFrameworkCore;



public class SkillRepository : ISkillRepository
{
    private readonly AppDbContext _context;
    public SkillRepository(AppDbContext context){
        _context = context;
    }
    public async Task<List<Skill>> GetAllSkillAsync()
    {
        return await _context.Skills.ToListAsync();
    }
}
