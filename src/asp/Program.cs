using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddProblemDetails(options =>
{
    options.CustomizeProblemDetails = ctx =>
    {
       ctx.ProblemDetails.Extensions["traceId"] = Activity.Current?.Id ??   ;
    };
});

var app = builder.Build();

app.MapPost("/", () => { }).WithName("GetWeatherForecast");

app.Run();