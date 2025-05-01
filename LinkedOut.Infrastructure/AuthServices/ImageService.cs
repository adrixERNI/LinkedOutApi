using LinkedOutApi.Interfaces;

namespace LinkedOutApi.Services;

public class ImageService : IImageService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public ImageService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<Stream> DownloadImageAsync(string imageUrl)
    {
        var http = _httpClientFactory.CreateClient();
        return await http.GetStreamAsync(imageUrl);
    }
}
