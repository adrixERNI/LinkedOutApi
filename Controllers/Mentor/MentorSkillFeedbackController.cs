using AutoMapper;
using LinkedOutApi.DTOs.User;
using LinkedOutApi.Entities;
using LinkedOutApi.Interfaces.User;
using Microsoft.AspNetCore.Mvc;

namespace LinkedOutApi.Controllers.Mentor
{
    [Route("api/[controller]")]
    [ApiController]
    public class MentorSkillFeedbackController : ControllerBase
    {
        private readonly IMentorSkillFeedbackRepository _repository;
        private readonly IMapper _mapper;

        public MentorSkillFeedbackController(IMentorSkillFeedbackRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<List<MentorSkillFeedback>>> AddSkillFeedback(int id, [FromBody] PostSkillFeedbackDTO dto)
        {
            var skillFeedback = _mapper.Map<List<MentorSkillFeedback>>(dto.SkillFeedback);

            skillFeedback.ForEach(sf => sf.MentorAssessmentId = dto.MentorAssessmentId);

            var result = await _repository.AddSkillFeedback(skillFeedback);
            return Ok(result);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<MentorSkillFeedback?>> UpdateSkillFeedback(int id, MentorSkillFeedback mentorSkillFeedback)
        {
            var currentSkillFeedback = _mapper.Map<MentorSkillFeedback>(mentorSkillFeedback);
            currentSkillFeedback.Id = id;

            var changedSkillFeedback = await _repository.UpdateSkillFeedback(id, currentSkillFeedback);

            if (changedSkillFeedback == null)
            {
                return NotFound();
            }
            return changedSkillFeedback;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<MentorSkillFeedback>> DeleteSkillFeedback(int id)
        {
            var removeSkillFeedback = await _repository.DeleteSkillFeedback(id);
            if (removeSkillFeedback == null)
            {
                return NotFound();
            }

            return Ok(removeSkillFeedback);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<GetSkillFeedbackDTO>>> GetAllSkillFeedbacks(int id)
        {
            var getSkillFeedback = await _repository.GetAllSkillFeedbacks(id);
            var map = _mapper.Map<IEnumerable<GetSkillFeedbackDTO>>(getSkillFeedback);
            return Ok(map);
        }
    }
}
