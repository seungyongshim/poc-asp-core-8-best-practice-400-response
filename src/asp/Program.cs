using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);


builder.Services.Configure<RouteHandlerOptions>(o => o.ThrowOnBadRequest = true);

// ���� ���� ó���� ����
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails(options =>
{
    options.CustomizeProblemDetails = ctx =>
    {
        ctx.ProblemDetails.Extensions["traceId"] = Activity.Current?.Id ?? ctx.HttpContext.TraceIdentifier;
    };
});

var app = builder.Build();

// ���� ó�� �̵���� Ȱ��ȭ
app.UseExceptionHandler();

app.MapPost("/", (SampleDto dto) => { }).WithName("GetWeatherForecast");

app.Run();


record SampleDto
{
    public required string Name { get; init; }
    public int Age { get; init; }
}