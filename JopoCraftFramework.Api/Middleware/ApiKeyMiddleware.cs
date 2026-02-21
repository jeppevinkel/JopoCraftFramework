using Microsoft.Extensions.Primitives;

namespace JopoCraftFramework.Api.Middleware;

public class ApiKeyMiddleware(RequestDelegate next, IConfiguration configuration)
{
    private const string ApiKeyHeader = "X-Api-Key";

    public async Task InvokeAsync(HttpContext context)
    {
        if (!context.Request.Headers.TryGetValue(ApiKeyHeader, out StringValues extractedApiKey))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("API key missing.");
            return;
        }

        var configuredApiKey = configuration["ApiKey"];
        if (!string.Equals(configuredApiKey, extractedApiKey, StringComparison.Ordinal))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Invalid API key.");
            return;
        }

        await next(context);
    }
}
