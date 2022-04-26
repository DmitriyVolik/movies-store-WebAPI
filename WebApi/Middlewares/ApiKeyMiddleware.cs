namespace WebApiTasks.Middlewares;

public class ApiKeyMiddleware
{
    private readonly RequestDelegate _next;

    private readonly string _apiKey;

    public ApiKeyMiddleware(RequestDelegate next, string apiKey)
    {
        _next = next;
        _apiKey = apiKey;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Headers["api_key"] != _apiKey)
        {
            context.Response.StatusCode = 403;
            return;
        }

        await _next(context);
    }
}