using LinkedOutApi.Data;
using LinkedOutApi.DTOs.CV;
using LinkedOutApi.Entities;
using LinkedOutApi.Interfaces;
using LinkedOutApi.Profiles;
using Microsoft.AspNetCore.Mvc;

namespace LinkedOut.Api.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    public class CVController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ICVRepository _cvRepo;
        public CVController(AppDbContext context, ICVRepository cvRepo)
        {
            _context = context;
            _cvRepo = cvRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var cv = await _cvRepo.GetAllCVsAsync();
            var cvDto = cv.Select(c => c.ToGetCVDto());
            return Ok(cvDto); 
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            var cv = await _cvRepo.GetCVByIdAsync(id);
            if (cv == null)
            {
                return NotFound();
            }
            return Ok(cv.ToGetCVDto());
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateCV cvDto)
        {
            var cvModel = await cvDto.ToCVFromCreateDtoAsync();
            await _cvRepo.AddCVAsync(cvModel);
            return CreatedAtAction(nameof(GetById), new {id = cvModel.Id}, cvModel.ToGetCVDto());
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCV cvDto)
        {
            var cv = await _cvRepo.GetCVByIdAsync(id);
            if (cv == null)
            {
                return NotFound();
            }
            cv.Name = cvDto.Name;
            cv.UserId = cvDto.UserId;
            await _cvRepo.UpdateCVAsync(cv);
            return NoContent();
        }
        [HttpDelete]   
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var cv = await _cvRepo.GetCVByIdAsync(id);
            if (cv == null)
            {
                return NotFound();
            }
            await _cvRepo.DeleteCVAsync(id);
            return NoContent();
        }
    }
}