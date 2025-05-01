using System;
using AutoMapper;
using LinkedOutApi.DTOs.Certifications;
using LinkedOutApi.DTOs.Response;
using LinkedOutApi.DTOs.User.TraineeFolder;
using LinkedOutApi.Entities;
using LinkedOutApi.Interfaces.Admin;
using LinkedOutApi.Interfaces.Cert;
using LinkedOutApi.Interfaces.Common;
using LinkedOutApi.Repositories.UserRepostiory;
using Microsoft.AspNetCore.Mvc;

namespace LinkedOutApi.Controllers.Certifications;

[Route("api/[controller]")]
[ApiController]
public class CertificationController:ControllerBase
{
    private readonly ICertificationRepository _certRepo;

    private readonly ISkillRepository _skillRepo;
    private readonly IMapper _mapper;

      private readonly IBatchService _batchService;

    private readonly IUserRepository _traineeRepo;
    public CertificationController(ICertificationRepository certRepo, ISkillRepository skillRepo, IMapper mapper, IBatchService batchService, IUserRepository traineeRepo){
        _certRepo = certRepo;
        _skillRepo = skillRepo;
        _mapper = mapper;
        _batchService = batchService;
        _traineeRepo = traineeRepo;

    }

    [HttpPost("trainee")]
    [ProducesResponseType(typeof(SuccessResponseDTO), 201)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 500)]
    public async Task<IActionResult> AddCertification([FromBody] CertificationsAddDTO createCertDTO){
        try{

            if(!await _skillRepo.SkillExistingAsync(createCertDTO.SkillId)){
                return NotFound(new {message = "Skill not found"});
            }
            var cert = _mapper.Map<Certification>(createCertDTO);
            var newCert = await _certRepo.CreateAsync(cert);
            return Ok(_mapper.Map<CertificationsAddDTO>(newCert));

        }catch(Exception ex){
            return StatusCode(500, new ErrorResponseDTO
            {
                Message = ex.Message,
                Success = false,
                StatusCode = 500
            });
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> CertificationGetById(int id){
        try{
            var certification = await _certRepo.GetByIdCertificationAsync(id);
            if(certification == null){
                return NotFound(new {message = "Id not found"});
            }
            var certificationDTO = _mapper.Map<CertificationResponseDTO>(certification);
            return Ok(certificationDTO);

        }catch(Exception ex){
            return StatusCode(500, new ErrorResponseDTO{
                Message = ex.Message,
                Success = false,
                StatusCode = 500
            });
        }

    }

    [HttpPut("user/trainee/{id}")]
    [ProducesResponseType(typeof(SuccessResponseDTO), 201)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 500)]
    public async Task<IActionResult> UpdateCertification([FromRoute] int id, [FromBody] CertificationUpdateDTO certUpdateDTO){
        try{
            if(!await _skillRepo.SkillExistingAsync(certUpdateDTO.SkillId)){
                return NotFound(new {message = "Skill not found"});
            }
            var cert = await _certRepo.UpdateCertificationAsync(id, certUpdateDTO);
            if(cert == null){
                return NotFound(new {message = "Certification not Found"});
            }

            var responseDto = _mapper.Map<CertificationResponseDTO>(cert);

            return Ok(responseDto);

        }catch(Exception ex){
            return StatusCode(500, new ErrorResponseDTO{
                Message = ex.Message,
                Success = false,
                StatusCode = 500
            });
        }
       
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(SuccessResponseDTO), 201)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 500)]
    public async Task<IActionResult> DeleteCertification([FromRoute] int id){
        try{
            var certification  = await _certRepo.DeleteCertificationAsync(id);

            if(certification == null){
                return NotFound(new {message = "Certification Not found"});
            }

            return Ok(new SuccessResponseDTO
                {
                    Message = "Certification Deleted Successfully",
                    Success = true,
                    StatusCode = 201
                });

        }catch(Exception ex){
            return StatusCode(500, new ErrorResponseDTO{
                Message = ex.Message,
                Success = false,
                StatusCode = 500
            });
        }
    }

     [HttpGet("user/trainee/{id}")]
    public async Task<IActionResult>GetByIdUserTraineeCertification(Guid id){
        var user = await _traineeRepo.GetByIdTraineeCertificationAsync(id);
        if(user == null){
            return NotFound(new {message = "User not found"});
        }

        var userDto = _mapper.Map<UserTraineeCertificationDTO>(user);
        return Ok(userDto);
    }

    // [HttpGet("trainee/{userId}/certification/{certificationId}")]
    [HttpGet("trainee/{userId}")]
    public async Task<IActionResult>GetByUserTraineeAndCertification([FromRoute] Guid userId, [FromQuery] int certificationId){

        
        var user = await _traineeRepo.GetByIdTraineeAndCertificationAsync(userId,certificationId);
        if(user == null){
            return NotFound(new {message = "User/Certification is not found"});
        }

        var userDto = _mapper.Map<UserTraineeCertificationDTO>(user);
        return Ok(userDto);
    }

}
