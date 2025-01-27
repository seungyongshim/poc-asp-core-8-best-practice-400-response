using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);


builder.Services.Configure<RouteHandlerOptions>(o => o.ThrowOnBadRequest = true);

// 전역 예외 처리기 설정
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails(options =>
{
    options.CustomizeProblemDetails = ctx =>
    {
        ctx.ProblemDetails.Extensions["traceId"] = Activity.Current?.Id ?? ctx.HttpContext.TraceIdentifier;
    };
});

var app = builder.Build();

// 예외 처리 미들웨어 활성화
app.UseExceptionHandler();

app.MapPost("/", (SampleDto dto) => { }).WithName("GetWeatherForecast");

app.Run();


record SampleDto
{
    public required string Name { get; init; }
    public int Age { get; init; }
}