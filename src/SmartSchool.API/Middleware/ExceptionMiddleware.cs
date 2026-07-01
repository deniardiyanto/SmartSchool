using System.Text.Json;
using SmartSchool.API.Responses;
using SmartSchool.Application.Common.Exceptions;

namespace SmartSchool.API.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleException(context, ex);
        }
    }

    private static async Task HandleException(
        HttpContext context,
        Exception ex)
    {
        context.Response.ContentType = "application/json";

        int statusCode = ex switch
        {
            NotFoundException => StatusCodes.Status404NotFound,
            ConflictException => StatusCodes.Status409Conflict,
            BadRequestException => StatusCodes.Status400BadRequest,
            UnauthorizedException => StatusCodes.Status401Unauthorized,
            ForbiddenException => StatusCodes.Status403Forbidden,
            _ => StatusCodes.Status500InternalServerError
        };

        context.Response.StatusCode = statusCode;

        var response = ApiResponse<object>.Fail(ex.Message);

        var json = JsonSerializer.Serialize(response);

        await context.Response.WriteAsync(json);
    }
}