using System;
using LinkedOutApi.Data;
using Microsoft.EntityFrameworkCore;
using LinkedOutApi.Interfaces.Cert;


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
}
