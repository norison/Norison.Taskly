using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using Norison.Taskly.Identity.Api.Data;

using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

using Scalar.AspNetCore;

using Serilog;

using StackExchange.Redis;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.OpenTelemetry(options =>
    {
        options.ResourceAttributes = new Dictionary<string, object> { ["service.name"] = "Taskly-Todos" };
    })
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

var redis = ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("Redis")!);

builder.Services.AddDataProtection()
    .PersistKeysToStackExchangeRedis(redis, "DataProtection-Keys")
    .SetApplicationName("SharedAPI");

builder.Logging.ClearProviders();
builder.Services.AddSerilog();
builder.Services.AddOpenApi();
builder.Services.AddAuthorization();
builder.Services.AddProblemDetails();
builder.Services.AddIdentityApiEndpoints<IdentityUser>().AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("IdentityDb")));

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

await using (var scope = app.Services.CreateAsyncScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await dbContext.Database.MigrateAsync();
}

app.UseExceptionHandler();
app.MapScalarApiReference();
app.MapOpenApi();
app.MapIdentityApi<IdentityUser>();

app.Run();