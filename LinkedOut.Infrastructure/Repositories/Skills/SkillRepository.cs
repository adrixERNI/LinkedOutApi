using System;
using System.Reflection.Metadata.Ecma335;
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
       var existingSkill = await _context.Skills.FirstOrDefaultAsync(s => s.Id == id && s.CategoryId == 3);
        if(existingSkill == null){
            return null;
        }

        bool certificationChecker = await _context.Certifications.AnyAsync(c => c.SkillId == id);
        if(certificationChecker){
            return null;
        }
        _context.Skills.Remove(existingSkill);
        await _context.SaveChangesAsync();
        return existingSkill;
    }

    public async Task<Skill> UpdateSelfSkillAsync(int id, Skill skill)
    {
        var existingSelfSkill = await _context.Skills.FirstOrDefaultAsync(s => s.Id == id && s.CategoryId == 3);
        if(existingSelfSkill == null){
            return null;
        }

      existingSelfSkill.Name = skill.Name;

       await _context.SaveChangesAsync();
       return existingSelfSkill;


        //     var existingCert = await _context.Certifications.FirstOrDefaultAsync(c => c.Id == id);
        // if (existingCert == null)
        // {
        //     return null; 
        // }


        // existingCert.Name = cert.Name;
        // existingCert.IssuingOrg = cert.IssuingOrg;
        // existingCert.Expiration = cert.Expiration;
        // existingCert.SkillId = cert.SkillId;


        // await _context.SaveChangesAsync();

        // return existingCert;

    }

    public async Task<Skill> GetByIdSelfSkillAsync(int id)
    {
        return await _context.Skills.FindAsync(id);
    }
}
