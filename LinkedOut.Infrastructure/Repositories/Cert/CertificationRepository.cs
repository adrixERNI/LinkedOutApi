using System;
using LinkedOutApi.Data;
using Microsoft.EntityFrameworkCore;
using LinkedOutApi.Interfaces.Cert;
using LinkedOutApi.Entities;
using LinkedOutApi.DTOs.Certifications;
using AutoMapper;

namespace LinkedOutApi.Repositories.Cert;

public class CertificationRepository : ICertificationRepository
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;


    public CertificationRepository(AppDbContext context, IMapper mapper){
        _context = context;
        _mapper = mapper;
   
    }
    public async Task<Certification> CreateAsync(Certification certification)
    {
        await _context.Certifications.AddAsync(certification);
        await _context.SaveChangesAsync();
        return certification;
    }


    public async Task<Certification> DeleteCertificationAsync(int id)
    {
        var existingCert = await _context.Certifications.FirstOrDefaultAsync(c => c.Id== id);
        if(existingCert == null){
            return null;
        }
        _context.Certifications.Remove(existingCert);
        await _context.SaveChangesAsync();
        return existingCert;

    }

    public async Task<List<Certification>> GetAllCertificationAsync()
    {
        return await _context.Certifications.ToListAsync();
    }

    public async Task<Certification> GetByIdCertificationAsync(int id)
    {
        return await _context.Certifications.FindAsync(id);
            
    }


    public async Task<Certification> UpdateCertificationAsync(int id, Certification cert)
    {
        var existingCert = await _context.Certifications.FirstOrDefaultAsync(c => c.Id == id);
        if (existingCert == null)
        {
            return null; 
        }


        existingCert.Name = cert.Name;
        existingCert.IssuingOrg = cert.IssuingOrg;
        existingCert.Expiration = cert.Expiration;
        existingCert.SkillId = cert.SkillId;


        await _context.SaveChangesAsync();

        return existingCert;
    }

}
