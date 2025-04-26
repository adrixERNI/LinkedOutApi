using System;
using AutoMapper;
using LinkedOutApi.DTOs.User.TraineeFolder;
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
public async Task<IActionResult> GetAll()
{
        var userTrainee = await _traineeRepo.GetAllUserAsync();
        var userTraineeDto = _mapper.Map<List<UserTraineeDTO>>(userTrainee);

        return Ok(userTraineeDto);
    
}

[HttpGet("mentor")]
public async Task<IActionResult> GetAllMentor(){
    var userMentor = await _traineeRepo.GetAllMentorAsync();
    var userMentorDTO = _mapper.Map<List<UserMentorDTO>>(userMentor);

    return Ok(userMentorDTO);
}
}
