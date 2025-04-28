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

    [HttpPost("trainee")]
    public async Task<IActionResult> AddCertification([FromBody] CertificationsAddDTO createCertDTO){
        if(!await _skillRepo.SkillExistingAsync(createCertDTO.SkillId)){
            return NotFound(new {message = "Skill not found"});
        }
        var cert = _mapper.Map<Certification>(createCertDTO);
        var newCert = await _certRepo.CreateAsync(cert);
        return Ok(_mapper.Map<CertificationsAddDTO>(newCert));

    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCertification([FromRoute] int id, [FromBody] CertificationUpdateDTO certUpdateDTO){
        if(!await _skillRepo.SkillExistingAsync(certUpdateDTO.SkillId)){
            return NotFound(new {message = "Skill not found"});
        }
        var cert = await _certRepo.UpdateCertificationAsync(id, certUpdateDTO);
        if(cert == null){
            return NotFound(new {message = "Certification not Found"});
        }

        var responseDto = _mapper.Map<CertificationResponseDTO>(cert);

    return Ok(responseDto);
       
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCertification([FromRoute] int id){
        var certification  = await _certRepo.DeleteCertificationAsync(id);

        if(certification == null){
            return NotFound(new {message = "Certification Not found"});
        }

        return Ok(new {message = "Certification has been deleted"});
    }
}
