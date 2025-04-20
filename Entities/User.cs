using System.ComponentModel.DataAnnotations;

namespace LinkedOutApi.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Role { get; set; } = null!;
    public bool IsApproved { get; set; } = false;
}