using SmartSchool.API.Middleware;

namespace SmartSchool.API.Extensions;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseGlobalException(
        this IApplicationBuilder app)
    {
        return app.UseMiddleware<ExceptionMiddleware>();
    }
}