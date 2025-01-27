using Microsoft.AspNetCore.Diagnostics;
// 전역 예외 처리기 클래스
public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        logger.LogError(exception, "");

        await Results.Problem(
            detail: exception.Message,
            statusCode: StatusCodes.Status500InternalServerError
        ).ExecuteAsync(httpContext);

        return true;
    }
}