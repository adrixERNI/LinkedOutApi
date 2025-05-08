using System;
using AutoMapper;
using LinkedOutApi.DTOs.Response;
using LinkedOutApi.DTOs.Skills;
using LinkedOutApi.Entities;
using LinkedOutApi.Interfaces.Admin;
using LinkedOutApi.Interfaces.Common;
using LinkedOutApi.Interfaces.User;
using Microsoft.AspNetCore.Mvc;

namespace LinkedOutApi.Controllers.Skills;

[Route("api/[Controller]")]
[ApiController]
public class PersonalSkillController:ControllerBase
{
    private readonly ISkillRepository _skillRepo;
    private readonly IMapper _mapper;

    private readonly IUserSkillRepository _userSkillRepo;
    private readonly IBatchRepository _batchService;



    public PersonalSkillController(ISkillRepository skillRepo, IMapper mapper, IBatchRepository batchService, IUserSkillRepository userSkillRepo){
        _skillRepo = skillRepo;
        _mapper = mapper;
        _batchService = batchService;
        _userSkillRepo = userSkillRepo;
    }

    [HttpGet("User")]
    [ProducesResponseType(typeof(SuccessResponseDTO), 200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 500)]
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
    [ProducesResponseType(typeof(SuccessResponseDTO), 200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 500)]
    public async Task<ActionResult<SelfSkillDTO>> GetAllSelfSkill(){
        try{
        var skills = await _skillRepo.GetAllSkillSelfAsync();
        var skill_dto = _mapper.Map<List<SelfSkillDTO>>(skills);
        return Ok(skill_dto);
        }catch(Exception ex){
            return StatusCode(500, new ErrorResponseDTO{
                Message = ex.Message,
                Success = false,
                StatusCode = 500
            });
        }
    }
    
    
    [HttpGet("{id}")]
    
    public async Task<IActionResult> CertificationGetById(int id){
    
            var skill = await _skillRepo.GetByIdSelfSkillAsync(id);
            if(skill == null){
                return NotFound(new {message = "Skill Id not found"});
            }
            var skillDTO = _mapper.Map<SelfSkillDTO>(skill);
            return Ok(skillDTO);

    
    }

    [HttpPost("Add")]
    public async Task<IActionResult> AddPersonalSkill([FromBody] SelfSkillAddDTO selfSkillAddDTO, Guid userId){
        var skill = _mapper.Map<Skill>(selfSkillAddDTO);
        if(skill.CategoryId != 3){
            return NotFound(new {message = "Category should fall under self"});
        }
        
        var newSkill = await _skillRepo.CreateSelfSkillAsync(skill);

        var userSkills  = new UserSkill{
            UserId = userId,
            SkillId = newSkill.Id,
            Rating = 0,
            IsDeleted = false

        };
        var userSkillList = new List<UserSkill>();
        userSkillList.Add(userSkills);
        await _userSkillRepo.AddUserSkill(userSkillList);
        return Ok(_mapper.Map<SelfSkillAddDTO>(newSkill));
    }

    // [HttpDelete("{id}")]
    // public async Task<IActionResult> DeletePersonalSkill([FromRoute] int id){

    //     var certification  = await _skillRepo.DeleteSelfSkillAsync(id);

    //     if(certification == null){
    //         return NotFound(new {message = "Skill may not be deleted. It may not exist or is linked to certifications."});
    //     }

    //     await _skillRepo.DeleteSelfSkillAsync(id);
    //     return Ok(new {message = "Skill deleted"});
    // }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePersonalSkill([FromRoute] int id, [FromBody] SelfSkillUpdateDTO skillUpdateDTO){
        
        var mappedSkill = _mapper.Map<Skill>(skillUpdateDTO);
        var skill = await _skillRepo.UpdateSelfSkillAsync(id, mappedSkill);
   
        if(skill == null){
                return NotFound(new {message = "Certification not Found"});
        }
        
        var skillDTo = _mapper.Map<SelfSkillDTO>(skill);
        // if(skillDTo.CategoryId !=3 ){
        //     return NotFound(new {message = "Category should fall under self"});
        // }
        return Ok(skillDTo);

    }
}
