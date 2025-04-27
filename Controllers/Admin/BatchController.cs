using AutoMapper;
using LinkedOutApi.DTOs.Response;
using LinkedOutApi.DTOs.User;
using LinkedOutApi.DTOs.User.UserFolder.BatchFolder;
using LinkedOutApi.Entities;
using LinkedOutApi.Interfaces.Admin;
using LinkedOutApi.Interfaces.User;
using LinkedOutApi.Services.Admin;
using Microsoft.AspNetCore.Mvc;

namespace LinkedOutApi.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class BatchController : ControllerBase
    {
        private readonly IBatchService _batchService;

        public BatchController(IBatchService batchService)
        {
            _batchService = batchService;
        }

        [HttpPost("/api/admin/batch")]
        [ProducesResponseType(typeof(SuccessResponseDTO), 200)]
        [ProducesResponseType(typeof(ErrorResponseDTO), 500)]
        public async Task<IActionResult> AddBatch(BatchCreateDTO batchCreateDTO)
        {
            try
            {
                var createdBatch = await _batchService.CreateBatchAsync(batchCreateDTO);
                return Ok(new SuccessResponseDTO
                {
                    Message = "Batch Created Successfully",
                    Success = true,
                    StatusCode = 200
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorResponseDTO
                {
                    Message = ex.Message,
                    Success = false,
                    StatusCode = 500
                });
            }
        }

        [HttpPut("/api/admin/batch/{id}")]
        [ProducesResponseType(typeof(SuccessResponseDTO), 201)]
        [ProducesResponseType(typeof(ErrorResponseDTO), 404)]
        [ProducesResponseType(typeof(ErrorResponseDTO), 500)]
        public async Task<IActionResult> UpdateBatch(int id, BatchReadDTO batchUpdateDTO)
        {
            try
            {
                var updatedBatch = await _batchService.UpdateBatchAsync(id, batchUpdateDTO);
                return Ok(new SuccessResponseDTO
                {
                    Message = "Batch Updated Successfully",
                    Success = true,
                    StatusCode = 201
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new ErrorResponseDTO
                {
                    Message = ex.Message,
                    Success = false,
                    StatusCode = 404
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorResponseDTO
                {
                    Message = ex.Message,
                    Success = false,
                    StatusCode = 500
                });
            }
        }

        [HttpGet("/api/admin/batch/{id}")]
        [ProducesResponseType(typeof(SuccessResponseDTO), 201)]
        [ProducesResponseType(typeof(ErrorResponseDTO), 404)]
        [ProducesResponseType(typeof(ErrorResponseDTO), 500)]
        public async Task<ActionResult<BatchReadDTO>> GetBatchById(int id)
        {
            try
            {
                var batch = await _batchService.GetBatchByIdAsync(id);
                return Ok(batch);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new ErrorResponseDTO
                {
                    Message = ex.Message,
                    Success = false,
                    StatusCode = 404
                });
            }
        }

        [HttpGet("/api/admin/batches/")]
        [ProducesResponseType(typeof(SuccessResponseDTO), 201)]
        [ProducesResponseType(typeof(ErrorResponseDTO), 500)]
        public async Task<ActionResult<BatchReadUserTopicDTO>> GetAllBatches()
        {
            try
            {
                var batches = await _batchService.GetBatchesAsync();
                return Ok(batches);
            }
            catch (Exception ex)
            {
                return StatusCode(500,new ErrorResponseDTO
                {
                    Message = ex.Message,
                    Success = false,
                    StatusCode = 500
                });
            }
        }
    }
}
