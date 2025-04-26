using System;
using System.ComponentModel.DataAnnotations;
using LinkedOutApi.DTOs.User.UserFolder.BatchFolder;
using LinkedOutApi.DTOs.User.UserFolder.ImageFolder;

namespace LinkedOutApi.DTOs.User.TraineeFolder;

public class UserMentorDTO
{
    public string Name {get; set;}
    public string Position {get; set;}
    public string Email {get; set;}
    public BatchDTO Batch { get; set; }

    //public ImageDTO Image {get; set;}

}
