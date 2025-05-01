using System;
using AutoMapper;
using LinkedOutApi.DTOs.Response;
using LinkedOutApi.DTOs.Skills;
using LinkedOutApi.Entities;
using LinkedOutApi.Interfaces.Admin;
using LinkedOutApi.Interfaces.Common;
using Microsoft.AspNetCore.Mvc;

namespace LinkedOutApi.Controllers.Skills;

[Route("api/[Controller]")]
[ApiController]
public class PersonalSkillController:ControllerBase
{
    private readonly ISkillRepository _skillRepo;
    private readonly IMapper _mapper;

    private readonly IBatchRepository _batchService;



    public PersonalSkillController(ISkillRepository skillRepo, IMapper mapper, IBatchRepository batchService){
        _skillRepo = skillRepo;
        _mapper = mapper;
        _batchService = batchService;
    }

    [HttpGet("User")]
    public async Task<ActionResult<SkillDTO>> GetAllUser(){
        try{
            var skills = await _skillRepo.GetAllSkillAsync();
            var skill_dto = _mapper.Map<List<SkillDTO>>(skills);
            return Ok(skill_dto);

        }catch(Exception ex){
            return StatusCode(500,new ErrorResponseDTO{
                Message = ex.Message,
                Success = false,
                StatusCode = 500
            });
        }
    }

    [HttpGet]
    public async Task<ActionResult<SelfSkillDTO>> GetAllSelfSkill(){
        var skills = await _skillRepo.GetAllSkillSelfAsync();
        var skill_dto = _mapper.Map<List<SelfSkillDTO>>(skills);
        return Ok(skill_dto);
    }


    [HttpPost]
    public async Task<IActionResult> AddPersonalSkill([FromBody] SelfSkillAddDTO selfSkillAddDTO){
        var skill = _mapper.Map<Skill>(selfSkillAddDTO);
        var newSkill = await _skillRepo.CreateSelfSkillAsync(skill);
        return Ok(_mapper.Map<SelfSkillAddDTO>(newSkill));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePersonalSkill([FromRoute] int id){

            var certification  = await _skillRepo.DeleteSelfSkillAsync(id);

            if(certification == null){
                return NotFound(new {message = "Skill Not found"});
            }

            await _skillRepo.DeleteSelfSkillAsync(id);
            return NoContent();
    }
}
