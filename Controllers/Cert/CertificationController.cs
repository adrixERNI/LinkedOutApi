using System;
using AutoMapper;
using LinkedOutApi.DTOs.Certifications;
using LinkedOutApi.Entities;
using LinkedOutApi.Interfaces.Cert;
using LinkedOutApi.Interfaces.Common;
using Microsoft.AspNetCore.Mvc;

namespace LinkedOutApi.Controllers.Certifications;

[Route("api/[controller]")]
[ApiController]
public class CertificationController:ControllerBase
{
    private readonly ICertificationRepository _certRepo;

    private readonly ISkillRepository _skillRepo;
    private readonly IMapper _mapper;

    public CertificationController(ICertificationRepository certRepo, ISkillRepository skillRepo, IMapper mapper){
        _certRepo = certRepo;
        _skillRepo = skillRepo;
        _mapper = mapper;

    }

    [HttpPost]
    public async Task<IActionResult> AddCertification([FromBody] CertificationsAddDTO createCertDTO){
        if(!await _skillRepo.SkillExistingAsync(createCertDTO.SkillId)){
            return NotFound(new {message = "Skill not found"});
        }
        var cert = _mapper.Map<Certification>(createCertDTO);
        var newCert = await _certRepo.CreateAsync(cert);
        return Ok(_mapper.Map<CertificationsAddDTO>(newCert));

    }
}
