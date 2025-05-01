namespace LinkedOutApi.DTOs.Response;

public class SuccessResponseDTO
{
    public bool Success { get; set; } = true;
    public string Message { get; set; } = string.Empty;
    public int StatusCode { get; set; }
}