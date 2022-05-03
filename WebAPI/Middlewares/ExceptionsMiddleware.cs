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
        catch (NotFoundException e)
        {
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            await context.Response.WriteAsync(e.Message);
        }
        catch (IncorrectDataException e)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            await context.Response.WriteAsync(e.Message);
        }
        catch (DbUpdateException e)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            await context.Response.WriteAsync(e.InnerException!.Message);
        }
        catch (Exception e)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsync(e.Message);
        }
    }
}