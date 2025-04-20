namespace LinkedOutApi.Exceptions;

public class HttpResponseException : Exception
{
    public int StatusCode { get; }
    public bool Success { get; } = false;

    public HttpResponseException(string message, int statusCode) : base(message)
    {
        StatusCode = statusCode;
    }
}