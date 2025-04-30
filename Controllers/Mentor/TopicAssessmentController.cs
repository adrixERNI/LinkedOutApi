using AutoMapper;
using LinkedOutApi.DTOs.User;
using LinkedOutApi.Entities;
using LinkedOutApi.Interfaces.User;
using Microsoft.AspNetCore.Mvc;

namespace LinkedOutApi.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicAssessmentController : ControllerBase
    {
        private readonly ITopicAssessmentRepository _mentorAssessmentRepository;
        private readonly IMapper _mapper;

        public TopicAssessmentController(ITopicAssessmentRepository mentorAssessmentRepository, IMapper mapper)
        {
            _mentorAssessmentRepository = mentorAssessmentRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<CreateAssessmentDTO>> AddTopicAssessment(CreateAssessmentDTO assessmentDTO)
        {
            var mapAssessment = _mapper.Map<TopicAssessment>(assessmentDTO);

            var newAssessment = await _mentorAssessmentRepository.AddTopicAssessment(mapAssessment);

            var result = _mapper.Map<CreateAssessmentDTO>(newAssessment);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<CreateAssessmentDTO>> DeleteTopicAssessment(int id)
        {
            var removeAssessment = await _mentorAssessmentRepository.DeleteTopicAssessment(id);
            if (removeAssessment == null) 
            {
                return NotFound();
            }
            return Ok(removeAssessment);
        }
        [HttpGet]
        public async Task<IEnumerable<TopicAssessment>> GetAllTopicAssessments()
        {
            var list = await _mentorAssessmentRepository.GetAllTopicAssessments();
            return list;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<CreateAssessmentDTO>> GetTopicAssessmentById(int id)
        {
            var getAssessment = await _mentorAssessmentRepository.GetTopicAssessmentById(id);
            var mapAssessment = _mapper.Map<CreateAssessmentDTO>(getAssessment);
            return Ok(mapAssessment);
        }
        
    }
}
