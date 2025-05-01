namespace LinkedOutApi.Interfaces;

public interface IImageService
{
    Task<Stream> DownloadImageAsync(string imageUrl);
}
