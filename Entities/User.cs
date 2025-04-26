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

    public int BatchId { get; set; }
    public int ImageId { get; set; }
    public int RoleId { get; set; }
    public int CVId { get; set; }
    public string Bio {get; set;}

    public string Position {get; set;}
    

    public ICollection<UserSkill> UserSkills { get; set; }

    public Batch Batch { get; set; }
    public Image Image { get; set; }
    public Role Role { get; set; }
    public CV CV { get; set; }
}