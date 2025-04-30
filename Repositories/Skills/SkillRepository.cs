using System;
using AutoMapper;
using LinkedOutApi.Data;
using LinkedOutApi.DTOs.Skills;
using LinkedOutApi.Entities;
using LinkedOutApi.Interfaces.Common;
using Microsoft.EntityFrameworkCore;



public class SkillRepository : ISkillRepository
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    public SkillRepository(AppDbContext context, IMapper mapper){
        _context = context;
        _mapper = mapper;
    }

    public async Task<Skill> CreateSelfSkillAsync(Skill skill)
{
    if (skill.CategoryId != 3)
    {
        throw new InvalidOperationException("Needs to be self category (CategoryId must be 3).");
    }

    await _context.Skills.AddAsync(skill);
    await _context.SaveChangesAsync();
    return skill;
}


    public async Task<List<Skill>> GetAllSkillAsync()
    {
        return await _context.Skills.ToListAsync();
    }

    public async Task<List<Skill>> GetAllSkillSelfAsync()
    {
        return await _context.Skills
            .Where(u => u.CategoryId == 3)
            .ToListAsync();
    }

    public async Task<bool> SkillExistingAsync(int skillId)
    {
          return await _context.Skills.AnyAsync(s => s.Id == skillId);
    }

    public async Task<Skill> DeleteSelfSkillAsync(int id)
    {
       var existingSkill = await _context.Skills.FirstOrDefaultAsync(s => s.Id == id);
        if(existingSkill == null){
            return null;
        }
        _context.Skills.Remove(existingSkill);
        await _context.SaveChangesAsync();
        return existingSkill;
    }

    public async Task<Skill> UpdateSelfSkillAsync(int id, SelfSkillUpdateDTO skill)
    {
        var existingSelfSkill = await _context.Skills.FirstOrDefaultAsync(s => s.Id == id);
        if(existingSelfSkill == null){
            return null;
        }

       _mapper.Map(skill,existingSelfSkill);
       await _context.SaveChangesAsync();
       return existingSelfSkill;

    }
}
