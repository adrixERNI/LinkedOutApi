using System.ComponentModel.DataAnnotations;
using System.Data;

namespace LinkedOutApi.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string GoogleId { get; set; }
    public bool IsApproved { get; set; } = false;
    public bool IsDeleted { get; set; }

    public DateTime? CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }

    public int? BatchId { get; set; }

    public string? Bio {get; set;}

    public string? Position {get; set;}

    public int? RoleId { get; set; }


    public virtual ICollection<UserSkill> UserSkills { get; set; }
    public virtual ICollection<Certification> Certifications {get; set;}

    public Batch Batch { get; set; }
    public Role Role { get; set; }

    public ICollection<Project> Projects { get; set; } = new List<Project>();
    public ICollection<CV> CVs { get; set; } = new List<CV>();
}