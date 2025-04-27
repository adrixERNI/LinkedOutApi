using LinkedOutApi.Entities;

namespace LinkedOutApi.DTOs.User.UserFolder
{
    public class UserReadDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsDeleted { get; set; }
        public int BatchId { get; set; }
        public int RoleId { get; set; }

        public string? Position { get; set; }

        public string RoleName { get; set; }
    }

    public class UserProfileDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsDeleted { get; set; }
        public int BatchId { get; set; }
        public int RoleId { get; set; }
        public string Bio {  get; set; }

        public string? Position { get; set; }

        public string RoleName { get; set; }
    }
}
