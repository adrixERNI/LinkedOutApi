namespace LinkedOutApi.Entities;

public class UserCreateDTO
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Role { get; set; } = null!;
    public bool IsApproved { get; set; } = false;
}