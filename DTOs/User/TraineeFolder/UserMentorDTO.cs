using System;
using System.ComponentModel.DataAnnotations;
using LinkedOutApi.DTOs.User.TraineeFolder.BatchFolder;
using LinkedOutApi.DTOs.User.TraineeFolder.ImageFolder;

namespace LinkedOutApi.DTOs.User.TraineeFolder;

public class UserMentorDTO
{
    public string name {get; set;}
    public string position {get; set;}
    public string email {get; set;}
    public BatchDTO Batch { get; set; }

    public ImageDTO Image {get; set;}

}
