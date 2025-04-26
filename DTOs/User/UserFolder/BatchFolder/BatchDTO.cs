using System;

namespace LinkedOutApi.DTOs.User.UserFolder.BatchFolder;

public class BatchReadDTO
{
     public string Name { get; set; }
     public string Status { get; set; }
     public bool IsDeleted { get; set; }
}

public class BatchCreateDTO
{
    public string Name { get; set; }
    public string Status { get; set; }
}

public class BatchUpdateDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Status { get; set; }
}
