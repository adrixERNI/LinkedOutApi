namespace LinkedOutApi.DTOs.Response;

public class ErrorResponseDTO
{
    public bool Success { get; set; } = false;
    public string Message { get; set; } = string.Empty;
    public int StatusCode { get; set; }
}