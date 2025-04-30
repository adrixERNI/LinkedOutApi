using AutoMapper;
using LinkedOutApi.DTOs.User;
using LinkedOutApi.Entities;
using LinkedOutApi.Interfaces.User;
using Microsoft.AspNetCore.Mvc;

namespace LinkedOutApi.Controllers.Mentor
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillFeedbackController : ControllerBase
    {
        private readonly ISkillFeedbackRepository _repository;
        private readonly IMapper _mapper;

        public SkillFeedbackController(ISkillFeedbackRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<List<SkillFeedback>>> AddSkillFeedback(int id, [FromBody] PostSkillFeedbackDTO dto)
        {
            var skillFeedback = _mapper.Map<List<SkillFeedback>>(dto.SkillFeedback);

            skillFeedback.ForEach(sf =>
            {
                sf.TopicAssessmentId = dto.TopicAssessmentId;
                sf.UserId = dto.UserId;
            });

            var result = await _repository.AddSkillFeedback(skillFeedback);
            return Ok(result);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<SkillFeedback?>> UpdateSkillFeedback(int id, SkillFeedback mentorSkillFeedback)
        {
            var currentSkillFeedback = _mapper.Map<SkillFeedback>(mentorSkillFeedback);
            currentSkillFeedback.Id = id;

            var changedSkillFeedback = await _repository.UpdateSkillFeedback(id, currentSkillFeedback);

            if (changedSkillFeedback == null)
            {
                return NotFound();
            }
            return changedSkillFeedback;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<SkillFeedback>> DeleteSkillFeedback(int id)
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
