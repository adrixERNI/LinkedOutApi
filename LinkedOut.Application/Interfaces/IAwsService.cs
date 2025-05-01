namespace LinkedOutApi.Interfaces;

public interface IAwsService
{
    Task<string> UploadImageToS3Async(Stream imageStream, string fileKey, string contentType = "image/jpeg");
}
