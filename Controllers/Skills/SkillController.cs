using System;
using AutoMapper;
using LinkedOutApi.DTOs.Skills;
using LinkedOutApi.Interfaces.Common;
using Microsoft.AspNetCore.Mvc;

namespace LinkedOutApi.Controllers.Skills;

[Route("api/[Controller]")]
[ApiController]
public class SkillController:ControllerBase
{
    private readonly ISkillRepository _skillRepo;
    private readonly IMapper _mapper;

    public SkillController(ISkillRepository skillRepo, IMapper mapper){
        _skillRepo = skillRepo;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<SkillDTO>> GetAllUser(){
        var skills = await _skillRepo.GetAllSkillAsync();
        var skill_dto = _mapper.Map<List<SkillDTO>>(skills);
        return Ok(skill_dto);
    }


}
