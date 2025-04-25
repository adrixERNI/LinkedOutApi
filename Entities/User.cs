using System.ComponentModel.DataAnnotations;
using System.Data;

namespace LinkedOutApi.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    //public string Status { get; set; }
    public bool IsApproved { get; set; } = false;

    public string GoogleId { get; set; }

    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }

    public Guid BatchId { get; set; }
    public Guid ImageId { get; set; }
    public Guid RoleId { get; set; }
    public Guid CVId { get; set; }

    public ICollection<UserSkill> UserSkills { get; set; }

    public Batch Batch { get; set; }
    public Image Image { get; set; }
    public Role Role { get; set; }
    public CV CV { get; set; }
}