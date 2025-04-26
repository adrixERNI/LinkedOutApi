using AutoMapper;
using LinkedOutApi.DTOs.User;
using LinkedOutApi.Entities;
using LinkedOutApi.Interfaces.User;
using Microsoft.AspNetCore.Mvc;

namespace LinkedOutApi.Controllers.Bootcamper
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserSkillController : ControllerBase
    {
        private readonly IUserSkillRepository _userSkillRepository;
        private readonly IMapper _mapper;

        public UserSkillController(IUserSkillRepository userSkillRepository, IMapper mapper)
        {
            _userSkillRepository = userSkillRepository;
            _mapper = mapper;
        }

        [HttpPost("{userId}/skills")]
        public async Task<ActionResult<List<UserSkill>>> AddUserSkill(Guid userId, [FromBody] PostUserSkillDTO dto)
        {
            var userSkills = _mapper.Map<List<UserSkill>>(dto.SkillRatings);

            foreach (var userSkill in userSkills)
            {
                userSkill.UserId = userId;
            }

            var result = await _userSkillRepository.AddUserSkill(userSkills);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<UserSkill>> DeleteUserSkill(int id)
        {
            var removeUserSkill = await _userSkillRepository.DeleteUserSkill(id);
            if (removeUserSkill == null)
            {
                return NotFound();
            }
            return Ok(removeUserSkill);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<GetUserSkillDTO>>> GetAllUserSkills(Guid id)
        {
            var getUserSkillList = await _userSkillRepository.GetAllUserSkills(id);
            var map = _mapper.Map<IEnumerable<GetUserSkillDTO>>(getUserSkillList);
            return Ok(map);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<UserSkill>> UpdateUserSkill(int id, UserSkill userSkill)
        {
            var currentUserSkill = _mapper.Map<UserSkill>(userSkill);
            currentUserSkill.Id = id;

            var changedUserSkill = await _userSkillRepository.UpdateUserSkill(id, currentUserSkill);

            if (changedUserSkill == null)
            {
                return NotFound();
            }
            return changedUserSkill;
        }
    }
}
