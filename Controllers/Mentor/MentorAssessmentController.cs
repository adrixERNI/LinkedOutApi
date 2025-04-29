using AutoMapper;
using LinkedOutApi.DTOs.User;
using LinkedOutApi.Entities;
using LinkedOutApi.Interfaces.User;
using Microsoft.AspNetCore.Mvc;

namespace LinkedOutApi.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class MentorAssessmentController : ControllerBase
    {
        private readonly IMentorAssessmentRepository _mentorAssessmentRepository;
        private readonly IMapper _mapper;

        public MentorAssessmentController(IMentorAssessmentRepository mentorAssessmentRepository, IMapper mapper)
        {
            _mentorAssessmentRepository = mentorAssessmentRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<CreateAssessmentDTO>> AddMentorAssessment(CreateAssessmentDTO assessmentDTO)
        {
            var mapAssessment = _mapper.Map<MentorAssessment>(assessmentDTO);

            var newAssessment = await _mentorAssessmentRepository.AddMentorAssessment(mapAssessment);

            var result = _mapper.Map<CreateAssessmentDTO>(newAssessment);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<CreateAssessmentDTO>> DeleteMentorAssessment(int id)
        {
            var removeAssessment = await _mentorAssessmentRepository.DeleteMentorAssessment(id);
            if (removeAssessment == null) 
            {
                return NotFound();
            }
            return Ok(removeAssessment);
        }
        [HttpGet]
        public async Task<IEnumerable<MentorAssessment>> GetAllMentorAssessments()
        {
            var list = await _mentorAssessmentRepository.GetAllMentorAssessments();
            return list;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<CreateAssessmentDTO>> GetMentorAssessmentById(int id)
        {
            var getAssessment = await _mentorAssessmentRepository.GetMentorAssessmentById(id);
            var mapAssessment = _mapper.Map<CreateAssessmentDTO>(getAssessment);
            return Ok(mapAssessment);
        }
        
    }
}
