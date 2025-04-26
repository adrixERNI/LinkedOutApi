using System;
using LinkedOutApi.DTOs.User.TraineeFolder.BatchFolder;
using LinkedOutApi.DTOs.User.TraineeFolder.CVFolder;
using LinkedOutApi.DTOs.User.TraineeFolder.ImageFolder;

namespace LinkedOutApi.DTOs.User.TraineeFolder;

public class UserTraineeDTO
{
    public string Name { get; set; }
    public string Email { get; set; }
    public BatchDTO Batch { get; set; }
    public string Bio { get; set; }
    public ImageDTO Image {get; set;}
    public CvDTO Resume {get; set;}
}