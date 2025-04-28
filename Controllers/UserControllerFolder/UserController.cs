using System;
using AutoMapper;
using LinkedOutApi.DTOs.Response;
using LinkedOutApi.DTOs.User.TraineeFolder;
using LinkedOutApi.DTOs.User.UserFolder;
using LinkedOutApi.Repositories.UserRepostiory;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LinkedOutApi.Controllers.TraineeFolder;

[Route("api/[Controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserRepository _traineeRepo;
    private readonly IMapper _mapper;


    public UserController(IUserRepository traineeRepo, IMapper mapper){
        _traineeRepo = traineeRepo;
        _mapper = mapper;

    }

    [HttpGet("trainee")]
     [ProducesResponseType(typeof(SuccessResponseDTO), 200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 500)]
    public async Task<ActionResult<UserReadDTO>> GetAll()
    {
        try{
            var userTrainee = await _traineeRepo.GetAllUserAsync();
            var userTraineeDto = _mapper.Map<List<UserTraineeDTO>>(userTrainee);

            return Ok(userTraineeDto);

        }catch(Exception ex){
            return StatusCode(500, new ErrorResponseDTO{
                Message = ex.Message,
                Success = false,
                StatusCode = 500
            });
        }
    
    }

    [HttpGet("mentor")]
    public async Task<IActionResult> GetAllMentor(){
        try{
            var userMentor = await _traineeRepo.GetAllMentorAsync();
            var userMentorDTO = _mapper.Map<List<UserMentorDTO>>(userMentor);

        return Ok(userMentorDTO);
        }catch(Exception ex){
            return StatusCode(500, new ErrorResponseDTO{
                Message = ex.Message,
                Success = false,
                StatusCode = 500
            });
        }
    }

    [HttpPost("/api/admin/batch/{batchId}/add/user")]
    [ProducesResponseType(typeof(SuccessResponseDTO), 200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 500)]
    public async Task<IActionResult> AddUsersToBatch(int batchId, List<Guid> userIds)
    {
        var result = await _traineeRepo.AddUsersToBatch(batchId, userIds);
        if (result)
        {
            return Ok(new SuccessResponseDTO
            {
                Message = "User/s Added to Batch",
                Success = true,
                StatusCode = 200
            });
        }
        return StatusCode(500, new ErrorResponseDTO
        {
            Message = "Something went wrong",
            Success = false,
            StatusCode = 500
        });
    }

    [HttpDelete("/api/admin/batch/{batchId}/remove/user")]
    [ProducesResponseType(typeof(SuccessResponseDTO), 200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 500)]
    public async Task<IActionResult> RemoveUsersFromBatch(int batchId, List<Guid> userIds)
    {
        var result = await _traineeRepo.RemoveUsersFromBatch(batchId, userIds);
        if (result)
        {
            return Ok(new SuccessResponseDTO
            {
                Message = "User/s removed from Batch",
                Success = true,
                StatusCode = 200
            });
        }
        return StatusCode(500, new ErrorResponseDTO
        {
            Message = "Something went wrong",
            Success = false,
            StatusCode = 500
        });
    }
}
