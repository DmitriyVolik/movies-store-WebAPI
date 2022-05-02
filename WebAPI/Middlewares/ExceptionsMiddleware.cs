using System.Net;
using Microsoft.EntityFrameworkCore;
using Models.Exceptions;

namespace WebAPI.Middlewares;

public class ExceptionsMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionsMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
            string message;
            
            switch (e)
            {
                case NotFoundException:
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    message = e.Message;
                    break;
                case IncorrectDataException:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    message = e.Message;
                    break;
                case DbUpdateException:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    message = e.InnerException!.Message;
                    break;
                default:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    message = e.Message;
                    break;
            }
            
            await context.Response.WriteAsync(message);
        }
    }
}