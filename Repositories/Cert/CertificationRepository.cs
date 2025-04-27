using System;
using LinkedOutApi.Data;
using Microsoft.EntityFrameworkCore;
using LinkedOutApi.Interfaces.Cert;
using LinkedOutApi.Entities;


namespace LinkedOutApi.Repositories.Cert;

public class CertificationRepository : ICertificationRepository
{
    private readonly AppDbContext _context;


    public CertificationRepository(AppDbContext context){
        _context = context;
   
    }
    public async Task<Entities.Certification> CreateAsync(Entities.Certification certification)
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
}
