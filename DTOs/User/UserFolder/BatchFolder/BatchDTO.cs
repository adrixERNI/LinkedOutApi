using System;
using LinkedOutApi.Entities;

namespace LinkedOutApi.DTOs.User.UserFolder.BatchFolder;

public class BatchReadDTO
{
     public string Name { get; set; }
     public string Status { get; set; }
     public bool IsDeleted { get; set; }
}

public class BatchReadUserTopicDTO
{
    public string Name { get; set; }
    public string Status { get; set; }
    public bool IsDeleted { get; set; }
    public ICollection<UserReadDTO> Users { get; set; }
    public ICollection<TopicReadDTO> Topics { get; set; }
}

public class BatchCreateDTO
{
    public string Name { get; set; }
    public string Status { get; set; }
}
