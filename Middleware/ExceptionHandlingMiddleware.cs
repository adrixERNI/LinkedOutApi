using LinkedOutApi.Exceptions;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (HttpResponseException ex)
        {
            await HandleHttpResponseExceptionAsync(context, ex);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleHttpResponseExceptionAsync(HttpContext context, HttpResponseException ex)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = ex.StatusCode;

        var response = new
        {
            success = ex.Success,
            message = ex.Message,
            statusCode = ex.StatusCode
        };

        return context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(response));
    }

    private Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = 500;

        var response = new
        {
            success = false,
            message = "An unexpected error occurred.",
            statusCode = 500
        };

        return context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(response));
    }
}