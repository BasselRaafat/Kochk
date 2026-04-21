using System.Net;
using System.Text.Json;
using Kochk.API.Errors;

namespace Kochk.API.Middlewares;

public class ExceptionMiddleware : IMiddleware
{
    private readonly ILogger logger;
    private readonly IHostEnvironment env;

    public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
    {
        this.logger = logger;
        this.env = env;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (InvalidOperationException ex)
        {
            logger.LogError(ex, "{message}", ex.Data);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "wfeiwfefjiewifjoen {message}", ex.Message);

            context.Response.ContentType = "application/json";

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var responsBody = env.IsDevelopment()
                ? new ApiExceptionError(500, ex.Message, ex.StackTrace?.ToString())
                : new ApiExceptionError(500);

            JsonSerializerOptions jsonSerializerOptions = new()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
            JsonSerializerOptions opts = jsonSerializerOptions;

            var json = JsonSerializer.Serialize(responsBody, opts);
            await context.Response.WriteAsync(json);
        }
    }
}
