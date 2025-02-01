namespace asp;
using Microsoft.AspNetCore.Diagnostics;

public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync
    (
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        await Results.Problem(
            instance: httpContext.Request.Path,
            detail: exception.Message
           
        ).ExecuteAsync(httpContext);

        return true;
    }
}