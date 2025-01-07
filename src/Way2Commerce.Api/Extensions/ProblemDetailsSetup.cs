using Microsoft.AspNetCore.Diagnostics;
using System.Text.Json;

namespace Way2Commerce.Api.Extensions;
public static class ProblemDetailsSetup
{
    private static Dictionary<Type, int> _mapping = new Dictionary<Type, int>
    {
        { typeof(UnauthorizedAccessException), StatusCodes.Status401Unauthorized },
        { typeof(JsonException), StatusCodes.Status400BadRequest },
        { typeof(ArgumentException), StatusCodes.Status400BadRequest },
        { typeof(ArgumentNullException), StatusCodes.Status400BadRequest },
        { typeof(NotImplementedException), StatusCodes.Status501NotImplemented },
        { typeof(HttpRequestException), StatusCodes.Status503ServiceUnavailable },
        { typeof(Exception), StatusCodes.Status500InternalServerError },
    };

    public static void AddApiProblemDetails(this IServiceCollection services)
    {
        services.AddProblemDetails(options =>
        {
            options.CustomizeProblemDetails = (context) => context.MapExceptionToStatusCode();
        });
    }

    public static void MapExceptionToStatusCode(this ProblemDetailsContext context)
    {
        var env = context.HttpContext.RequestServices.GetRequiredService<IHostEnvironment>();
        var exception = context.HttpContext.Features.Get<IExceptionHandlerPathFeature>()?.Error;

        if (exception is not null)
        {
            var statusCode = _mapping.GetValueOrDefault(exception.GetType(), context.HttpContext.Response.StatusCode);
            context.HttpContext.Response.StatusCode = statusCode;
            context.ProblemDetails.Status = statusCode;
            context.ProblemDetails.Detail = env.IsDevelopment() || env.IsStaging() ? context.ProblemDetails.Detail : null;
        }
    }
}