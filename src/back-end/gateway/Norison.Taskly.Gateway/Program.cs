using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.OpenTelemetry(options =>
    {
        options.ResourceAttributes = new Dictionary<string, object> { { "service.name", "Taskly-Gateway" } };
    })
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddReverseProxy().LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

builder.Services
    .AddOpenTelemetry()
    .ConfigureResource(resource => resource.AddService("Taskly-Gateway"))
    .WithTracing(options =>
    {
        options.AddHttpClientInstrumentation();
        options.AddAspNetCoreInstrumentation();
        options.AddOtlpExporter();
    })
    .WithMetrics(options =>
    {
        options.AddHttpClientInstrumentation();
        options.AddAspNetCoreInstrumentation();
        options.AddOtlpExporter();
    });

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(config => config.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

var app = builder.Build();
app.UseCors();
app.MapReverseProxy();
app.Run();