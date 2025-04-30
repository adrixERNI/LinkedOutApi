using System;

namespace LinkedOutApi.DTOs.Skills;

public class SkillDTO
{
    public int Id{get; set;}
    public string Name {get; set;}
}


public class SelfSkillDTO{
    public int Id{get; set;}
    public string Name {get; set;}

    public int CategoryId{get; set;}
}

public class SelfSkillAddDTO{
    public string Name {get; set;}
     public int CategoryId{get; set;}
}