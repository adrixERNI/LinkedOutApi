using Microsoft.AspNetCore.Mvc;
using LinkedOutApi.Data;
using LinkedOutApi.DTOs.Projects;
using LinkedOutApi.Interfaces;
using LinkedOutApi.Profiles;
using Microsoft.AspNetCore.Authorization;
namespace LinkedOutApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class ProjectController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IProjectRepository _projectRepo;
        public ProjectController(AppDbContext context, IProjectRepository projectRepo)
        {
            _context = context;
            _projectRepo = projectRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var project = await _projectRepo.GetAllProjects();
            
            var projectDto = project.Select(p => p.ToGetProjectsDto());

            return Ok(project); 
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            var project = await _projectRepo.GetProjectById(id);
            if (project == null)
            {
                return NotFound();
            }
            
            return Ok(project.ToGetProjectsDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProjects projectDto)
        {
            var projectModel = projectDto.ToProjectFromCreateDto();
            await _projectRepo.AddAsync(projectModel);
            return CreatedAtAction(nameof(GetById), new {id = projectModel.Id}, projectModel.ToGetProjectsDto());

        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateProjects projectDto)
        {
            var project = await _projectRepo.GetProjectById(id);
            if (project == null)
            {
                return NotFound();
            }
            
            project.Title = projectDto.Title;
            project.Description = projectDto.Description;
            project.TechUsed = projectDto.TechUsed;
            
            await _context.SaveChangesAsync();
            
            return Ok(project.ToGetProjectsDto());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var projectModel = await _projectRepo.GetProjectById(id);

            if (projectModel == null)
            {
                return NotFound();
            }
            
            await _projectRepo.DeleteAsync(id);
        
            return NoContent();
        }
    }
}