using System;
using AutoMapper;
using LinkedOutApi.DTOs.Response;
using LinkedOutApi.DTOs.Skills;
using LinkedOutApi.Interfaces.Admin;
using LinkedOutApi.Interfaces.Common;
using Microsoft.AspNetCore.Mvc;

namespace LinkedOutApi.Controllers.Skills;

[Route("api/[Controller]")]
[ApiController]
public class SkillController:ControllerBase
{
    private readonly ISkillRepository _skillRepo;
    private readonly IMapper _mapper;

    private readonly IBatchRepository _batchService;



    public SkillController(ISkillRepository skillRepo, IMapper mapper, IBatchRepository batchService){
        _skillRepo = skillRepo;
        _mapper = mapper;
        _batchService = batchService;
    }

    [HttpGet]
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


}
