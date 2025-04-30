using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinkedOutApi.Entities;

namespace LinkedOutApi.DTOs.Projects
{
    public class GetProjects
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string TechUsed { get; set; }
        public Guid UserId { get; set; }
        public bool IsDeleted { get; set; }
        public LinkedOutApi.Entities.User User { get; set; }
    }
}