namespace asp;
using Microsoft.AspNetCore.Diagnostics;

public class GlobalExceptionHandler
(
    ILogger<GlobalExceptionHandler> logger
) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync
    (
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        logger.LogError(exception, "");

        await Results.Problem(
            instance: httpContext.Request.Path,
            detail: exception.Message
           
        ).ExecuteAsync(httpContext);

        return true;
    }
}