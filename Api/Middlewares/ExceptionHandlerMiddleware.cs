using System.Net;

namespace api.Middlewares;
public class ExceptionHandlerMiddleware
{
    private readonly ILogger<ExceptionHandlerMiddleware> logger;
    private readonly RequestDelegate next;

    public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger,
        RequestDelegate next)
    {
        this.logger = logger;
        this.next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await next(httpContext);
        }
        catch(Exception ex)
        {
            var errorId = Guid.NewGuid();

            // Log the exception
            logger.LogError(ex, $"{errorId} : {ex.Message}");

            // Return a Custom error response
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            httpContext.Response.ContentType = "application/json";

            var error = new 
            {
                Id = errorId,
                ErrorMessage = "Something on our side went wrong. Apologize for inconvenience. We are alerted about this and will look into as soon as possible."
            };

            await httpContext.Response.WriteAsJsonAsync(error);
        }
    }
}