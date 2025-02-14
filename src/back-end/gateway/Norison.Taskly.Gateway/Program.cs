using System.Security.Claims;

using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;

using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

using Serilog;

using StackExchange.Redis;

using Yarp.ReverseProxy.Transforms;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.OpenTelemetry(options =>
    {
        options.ResourceAttributes = new Dictionary<string, object> { { "service.name", "Taskly-Gateway" } };
    })
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"))
    .AddTransforms(context =>
    {
        if (context.Route.AuthorizationPolicy == "default")
        {
            context.AddRequestTransform(async transformContext =>
            {
                var id = transformContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                transformContext.ProxyRequest.Headers.Add("X-User-Id", id);
                await Task.CompletedTask;
            });
        }
    });

var redis = ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("Redis")!);

builder.Services.AddDataProtection()
    .PersistKeysToStackExchangeRedis(redis, "DataProtection-Keys")
    .SetApplicationName("SharedAPI");

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

builder.Services
    .AddAuthentication(IdentityConstants.BearerScheme)
    .AddCookie(IdentityConstants.ApplicationScheme)
    .AddBearerToken(IdentityConstants.BearerScheme);

var app = builder.Build();

app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.MapReverseProxy();
app.Run();