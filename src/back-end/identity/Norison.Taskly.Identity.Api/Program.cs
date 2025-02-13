using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using Norison.Taskly.Identity.Api.Data;

using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

using Scalar.AspNetCore;

using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.OpenTelemetry(options =>
    {
        options.ResourceAttributes = new Dictionary<string, object> { ["service.name"] = "Taskly-Todos" };
    })
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Services.AddSerilog();
builder.Services.AddOpenApi();
builder.Services.AddAuthorization();
builder.Services.AddProblemDetails();
builder.Services.AddIdentityApiEndpoints<IdentityUser>().AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

builder.Services
    .AddOpenTelemetry()
    .ConfigureResource(resource => resource.AddService("Taskly-Identity"))
    .WithMetrics(options =>
    {
        options.AddHttpClientInstrumentation();
        options.AddAspNetCoreInstrumentation();
        options.AddOtlpExporter();
    })
    .WithTracing(options =>
    {
        options.AddHttpClientInstrumentation();
        options.AddAspNetCoreInstrumentation();
        options.AddEntityFrameworkCoreInstrumentation("identity-db", efOptions =>
        {
            efOptions.SetDbStatementForText = true;
            efOptions.SetDbStatementForStoredProcedure = true;
        });
        options.AddOtlpExporter();
    });

var app = builder.Build();

app.UseExceptionHandler();
app.MapScalarApiReference();
app.MapOpenApi();
app.MapIdentityApi<IdentityUser>();

app.Run();