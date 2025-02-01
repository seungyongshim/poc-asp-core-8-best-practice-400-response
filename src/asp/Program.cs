using System.Diagnostics;
using asp;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpLogging(o =>
{
    o.LoggingFields = HttpLoggingFields.All;
    o.CombineLogs = true;
});
builder.Services.Configure<RouteHandlerOptions>(o => o.ThrowOnBadRequest = true);
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddProblemDetails(options =>
{
    options.CustomizeProblemDetails = ctx =>
    {
        var exceptionHandlerFeature = ctx.HttpContext.Features.Get<IExceptionHandlerPathFeature>();
        if (exceptionHandlerFeature != null)
        {
            var exception = exceptionHandlerFeature.Error;
        }

            ctx.ProblemDetails.Extensions["traceId"] = Activity.Current?.Id ?? ctx.HttpContext.TraceIdentifier;
    };
});

var app = builder.Build();

app.UseHttpLogging();
app.UseExceptionHandler();
app.UseStatusCodePages();
app.MapPost("/", (SampleDto dto) => { });
app.Run();


record SampleDto
{
    public required string Name { get; init; }
    public int Age { get; init; }
}