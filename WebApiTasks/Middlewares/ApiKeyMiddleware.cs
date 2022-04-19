using Microsoft.AspNetCore.Mvc;

namespace WebApiTasks.Middlewares;

public class ApiKeyMiddleware
{
    private readonly RequestDelegate _next;
    
    private string _apiKey;
    
    public ApiKeyMiddleware(RequestDelegate next, string apiKey)
    {
        _next = next;
        _apiKey = apiKey;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Headers["ApiKey"] != _apiKey)
        {
            context.Response.StatusCode = 401;
            return;
        }

        await _next(context);
    }
}