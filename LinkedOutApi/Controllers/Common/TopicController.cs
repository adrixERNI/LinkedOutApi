using LinkedOutApi.DTOs.Response;
using LinkedOutApi.DTOs.Shared;
using LinkedOutApi.Interfaces.Common;
using Microsoft.AspNetCore.Mvc;

namespace LinkedOutApi.Controllers.Common
{
    [ApiController]
    [Route("api/[controller]")]
    public class TopicController : ControllerBase
    {
        private readonly ITopicService _topicService;

        public TopicController(ITopicService topicService)
        {
            _topicService = topicService;
        }

        [HttpPost("/api/admin/batch/{id}/topic")]
        [ProducesResponseType(typeof(SuccessResponseDTO), 201)]
        [ProducesResponseType(typeof(ErrorResponseDTO), 500)]
        public async Task<IActionResult> AddTopicWithSkill([FromBody] TopicAddRequestDTO topicAddRequestDTO)
        {
            // I Could also get the batch id from url and compare it to batchId in the dto to double check
            try
            {
                var topic = await _topicService.CreateTopicWithSkillAsync(topicAddRequestDTO);
                return Ok(new SuccessResponseDTO
                {
                    Success = true,
                    StatusCode = 201,
                    Message = "Topic Added"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorResponseDTO
                {
                    Success = false,
                    StatusCode = 500,
                    Message = ex.Message
                });
            }
        }

        [HttpPut("/api/admin/batch/{id}/topic/{topicId}")]
        [ProducesResponseType(typeof(SuccessResponseDTO), 200)]
        [ProducesResponseType(typeof(ErrorResponseDTO), 500)]
        public async Task<IActionResult> UpdateTopicWithSkill([FromRoute] int topicId, [FromBody] TopicAddRequestDTO topicDto)
        {
            // I Could also get the batch id from url and compare it to batchId in the dto to double check
            try
            {
                var topic = await _topicService.UpdateTopicAsync(topicId, topicDto);
                return Ok(new SuccessResponseDTO
                {
                    Success = true,
                    StatusCode = 200,
                    Message = "Topic Updated"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorResponseDTO
                {
                    Success = false,
                    StatusCode = 500,
                    Message = ex.Message
                });
            }
        }

        [HttpGet("/api/admin/batch/{id}/topic/{topicId}")]
        [ProducesResponseType(typeof(SuccessResponseDTO), 200)]
        [ProducesResponseType(typeof(ErrorResponseDTO), 404)]
        [ProducesResponseType(typeof(ErrorResponseDTO), 500)]
        public async Task<ActionResult<TopicReadDTO>> GetTopicById([FromRoute] int topicId)
        {
            try
            {
                var topic = await _topicService.GetTopicByIdAsync(topicId);

                return Ok(topic);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new ErrorResponseDTO
                {
                    Success = false,
                    StatusCode = 404,
                    Message = "Topic Not Found"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorResponseDTO
                {
                    Success = false,
                    StatusCode = 500,
                    Message = $"Something Went Wrong.\n{ex.Message}"
                });
            }
        }

        [HttpGet("/api/admin/batch/{id}/topic/")]
        [ProducesResponseType(typeof(SuccessResponseDTO), 200)]
        [ProducesResponseType(typeof(ErrorResponseDTO), 500)]
        public async Task<ActionResult<TopicReadDTO>> GetTopics()
        {
            try
            {
                var topics = await _topicService.GetTopicsAsync();
                return Ok(topics);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorResponseDTO
                {
                    Success = false,
                    StatusCode = 500,
                    Message = $"Something Went Wrong.\n{ex.Message}"
                });
            }
        }

        [HttpDelete("/api/admin/batch/{id}/topic/{topicId}")]
        [ProducesResponseType(typeof(SuccessResponseDTO), 200)]
        [ProducesResponseType(typeof(ErrorResponseDTO), 404)]
        [ProducesResponseType(typeof(ErrorResponseDTO), 500)]
        public async Task<IActionResult> DeleteTopicById([FromRoute] int topicId)
        {
            try
            {
                var deletedTopic = await _topicService.DeleteTopicAsync(topicId);
                return Ok(new SuccessResponseDTO
                {
                    Success = true,
                    StatusCode = 200,
                    Message = "Topic Deleted"
                });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new ErrorResponseDTO
                {
                    Success = false,
                    StatusCode = 404,
                    Message = "Topic Not Found"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorResponseDTO
                {
                    Success = false,
                    StatusCode = 500,
                    Message = $"Something Went Wrong.\n{ex.Message}"
                });
            }
        }
    }
}
