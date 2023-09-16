using System.Text.Json;
using FluentValidation;

namespace AuthService.API.Middlewares;

public class ErrorMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (Exception e)
        {
             await ExceptionHandler(e, context);
        }
    }

    private async Task ExceptionHandler(Exception exc, HttpContext context)
    {
        int statusCode = StatusCodes.Status500InternalServerError;
        var result = "error";

        switch (exc)
        {
            case ValidationException exception:
                statusCode = StatusCodes.Status400BadRequest;
                result = JsonSerializer.Serialize(exception.Errors);
                break;
        }

        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";

        await context.Response.WriteAsync(result);
    }
}