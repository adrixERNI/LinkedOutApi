using System.CodeDom;
using LinkedOutApi.DTOs.Projects;
using LinkedOutApi.Entities;

namespace LinkedOutApi.Profiles
{
    public static class ProjectsMapper
    {
        public static GetProjects ToGetProjectsDto(this Project projectModel){
            return new GetProjects
            {
                Id = projectModel.Id,
                Title = projectModel.Title,
                Description = projectModel.Description,
                TechUsed = projectModel.TechUsed,
                UserId = projectModel.UserId,
                IsDeleted = projectModel.IsDeleted
            };
        }

        public static Project ToProjectFromCreateDto(this CreateProjects createProjectDto)
        {
            return new Project
            {
                Title = createProjectDto.Title,
                Description = createProjectDto.Description,
                TechUsed = createProjectDto.TechUsed,
                UserId = createProjectDto.UserId
            };
        }
    }   
}